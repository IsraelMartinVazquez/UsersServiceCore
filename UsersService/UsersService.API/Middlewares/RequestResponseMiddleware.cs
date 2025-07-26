using System.Text;
using Newtonsoft.Json;
using UsersService.Domain.Entities;
using UsersService.Infrastructure.Repositories;

namespace UsersService.API.Middlewares;

/// <summary>
/// Middleware para registrar las solicitudes y respuestas HTTP entrantes en la API,
/// omitiendo recursos estáticos y solicitudes a Swagger. Guarda los datos en la base de datos.
/// </summary>
public class RequestResponseMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Constructor del middleware.
    /// </summary>
    /// <param name="next">Delegado que representa el siguiente middleware en la tubería.</param>
    public RequestResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Intercepta la solicitud y respuesta HTTP para registrar información relevante.
    /// </summary>
    /// <param name="httpContext">Contexto de la solicitud HTTP.</param>
    /// <returns>Tarea asincrónica que representa la operación del middleware.</returns>
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            var path = httpContext.Request.Path.Value?.ToLower() ?? "";
            bool isSwaggerRequest = path.StartsWith("/swagger");

            string? contentType = httpContext.Response.ContentType?.ToLower();
            bool isStaticAsset = contentType == "image/png" || contentType == "text/css";

            bool skipLogging = isSwaggerRequest || isStaticAsset;

            httpContext.Request.EnableBuffering();
            httpContext.Request.Body.Position = 0;

            using var requestBody = new MemoryStream();
            await httpContext.Request.Body.CopyToAsync(requestBody);
            httpContext.Request.Body = requestBody;

            var requestBodyString = Encoding.UTF8.GetString(requestBody.ToArray());
            var requestHeaders = httpContext.Request.Headers.ToDictionary();

            requestBody.Position = 0;
            httpContext.Request.Body = requestBody;

            var originalBodyResponse = httpContext.Response.Body;
            using var memoryStream = new MemoryStream();
            httpContext.Response.Body = memoryStream;

            await _next(httpContext);

            memoryStream.Position = 0;
            using var reader = new StreamReader(memoryStream);
            var responseBody = await reader.ReadToEndAsync();

            if (!skipLogging &&
                httpContext.Response.ContentType != null &&
                !string.IsNullOrWhiteSpace(responseBody) &&
                !responseBody.Contains("DOCTYPE"))
            {
                var logDBService = httpContext.RequestServices.GetService<LogRepository>();
                if (logDBService != null)
                {
                    await logDBService.InsertLogAsync(new Log
                    {
                        Domain = httpContext.Request.Host.ToString(),
                        Host = Environment.MachineName,
                        Method = httpContext.Request.Method,
                        Route = httpContext.Request.Path,
                        JsonHeaders = JsonConvert.SerializeObject(requestHeaders),
                        JsonRequest = JsonConvert.SerializeObject(requestBodyString),
                        JsonResponse = JsonConvert.SerializeObject(responseBody)
                    });
                }
            }

            memoryStream.Position = 0;
            await memoryStream.CopyToAsync(originalBodyResponse);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}