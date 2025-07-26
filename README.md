# UsersServiceCore

Servicio API RESTful desarrollado con .NET 8 que permite el registro y autenticación de usuarios utilizando JWT. Incluye documentación interactiva a través de Swagger.

## 🚀 Características

- .NET 8 + C#
- Arquitectura limpia (Clean Architecture)
- Endpoints para registro e inicio de sesión de usuarios
- Autenticación basada en JWT
- Documentación con Swagger
- Acceso a base de datos mediante procedimientos almacenados

---

## 🛠️ Tecnologías utilizadas

- ASP.NET Core 8
- Swagger / Swashbuckle
- ADO.NET
- SQL Server (procedimientos almacenados)
- JWT (System.IdentityModel.Tokens.Jwt)
- AutoMapper
- FluentValidation

---

## 📦 Estructura del proyecto

```plaintext
UsersServiceCore/
├── UsersService.API             # Capa de presentación (API)
├── UsersService.Application     # Casos de uso, DTOs, interfaces
├── UsersService.Domain          # Entidades del dominio
├── UsersService.Infrastructure  # Repositorios, acceso a datos
└── UsersService.Tests           # Pruebas unitarias
```

---

## 📌 Endpoints disponibles

### 🔐 POST `/users/register`

Registra un nuevo usuario en la base de datos.

**Request body:**
```json
{
  "FirstName": "Juan",
  "LastName": "Gomez Perez",
  "Email": "juan@example.com",
  "PhoneNumber": "5523456791",
  "passwordHash": "3117ef5bd0a1c3e..."
}
```

**Respuesta exitosa:**
```json
{
  "success": true,
  "message": "",
  "errors": [],
  "statusCode": 200
}
```

---

### 🔐 POST `/users/login`

Autentica a un usuario registrado y retorna un token JWT.

**Request body:**
```json
{
  "email": "juan@example.com",
  "passwordHash": "3117ef5bd0a1c3e..."
}
```

**Respuesta exitosa:**
```json
{
  "data": {
    "userId": 1,
    "firstName": "Juan",
    "lastName": "Gomez Perez"",
    "phoneNumber": "5523456791"
  },
  "success": true,
  "message": "",
  "errors": [],
  "statusCode": 200,
  "token": "eyJhbGciOiJIUzI1NiIsInR..."
}
```

---

## 🧪 Cómo probar

1. Clona el repositorio:

```bash
git clone https://github.com/IsraelMartinVazquez/UsersServiceCore.git
cd UsersServiceCore
```

2. Configura la cadena de conexión en `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CoreUsersDB;User Id=sa;Password=pass;TrustServerCertificate=true;"
  },
  "JwtSettings": {
    "SecretKey": "clave-secreta-segura",
    "Issuer": "UsersServiceAPI",
    "Audience": "UsersClient",
    "ExpirationMinutes": 60
  }
}
```

3. Ejecuta la API:

```bash
dotnet run --project UsersService.API
```

4. Abre Swagger en tu navegador:  
   👉 http://localhost:5000/swagger

---

## 🧰 Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022+ o VS Code
- SQL Server

---

## 🔒 Seguridad

- Los tokens JWT generados en el login deben incluirse en el header `Authorization` para endpoints protegidos:
  ```
  Authorization: Bearer {token}
  ```

---

## 📄 Licencia

Este proyecto está bajo la licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.

---

## 🙋 Autor

Desarrollado por **Israel Martín**  
🔗 GitHub: [github.com/IsraelMartinVazquez](https://github.com/IsraelMartinVazquez)
