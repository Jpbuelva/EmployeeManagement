 
# EmployeeManagement

## Descripción

EmployeeManagement es una aplicación ASP.NET Core que gestiona la información de los empleados, incluyendo su historial de posiciones, departamentos, proyectos y más. 
La aplicación sigue los principios SOLID y utiliza Entity Framework Core para la gestión de datos.

## Estructura del Proyecto

EmployeeManagement/
├── EmployeeManagement.Api/
│   ├── Connected Services/
│   ├── Dependencies/
│   ├── Properties/
│   ├── Controllers/
│   ├── appsettings.Development.json
│   ├── appsettings.json
│   └── Program.cs
├── EmployeeManagement.Application/
│   ├── Dependencies/
│   ├── Interfaces/
│   ├── Mapper/
│   └── Services/
├── EmployeeManagement.Domain/
│   ├── Dependencies/
│   ├── DTOs/
│   ├── Enums/
│   ├── Entities/
│   │   ├── Employee.cs
│   │   ├── RequestLog.cs
│   │   ├── Role.cs
│   │   └── User.cs
│   └── Models/
└── EmployeeManagement.Infrastructure/
    ├── Dependencies/
    ├── Data/
    ├── Interfaces/
    ├── Middleware/
    ├── Migrations/
    └── Persistence/

## Prerrequisitos

- .NET 8 SDK
- SQL Server
- Visual Studio

## Configuración del Proyecto

1. Clonar el repositorio

   git clone https://github.com/Jpbuelva/EmployeeManagement.git
   cd EmployeeManagement

2. Configurar la base de datos

   - Actualiza la cadena de conexión en el archivo `appsettings.json` en el proyecto `EmployeeManagement.Api`:

     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\sqllocaldb;Database=EmployeeManagement;Trusted_Connection=True;MultipleActiveResultSets=true"
     }

   - Aplica las migraciones y crea la base de datos:

     cd src/EmployeeManagement.Infrastructure
     dotnet ef database update

3. Ejecutar la aplicación

   - Navega hasta el directorio del proyecto `EmployeeManagement.Api` y ejecuta la aplicación:

     cd ../EmployeeManagement.Api
     dotnet run

   - La API estará disponible en `https://localhost:7134`.

4. Script SQL

   Es importante poblar la base de datos una vez se apliquen las migraciones.

   -- Insertar datos en la tabla Department
   INSERT INTO Departments (Name, Location) VALUES ('HR', 'Medellin');
   INSERT INTO Departments (Name, Location) VALUES ('IT', 'Cali');
   INSERT INTO Departments (Name, Location) VALUES ('Finance', 'Cartagena');
   INSERT INTO Departments (Name, Location) VALUES ('Marketing', 'Bogota');

   -- Insertar datos en la tabla Project
   INSERT INTO Projects (Name, Description) VALUES ('Project A', 'Description for Project A');
   INSERT INTO Projects (Name, Description) VALUES ('Project B', 'Description for Project B');
   INSERT INTO Projects (Name, Description) VALUES ('Project C', 'Description for Project C');
   INSERT INTO Projects (Name, Description) VALUES ('Project D', 'Description for Project D');

## Uso de la API

### Autenticación

- Loguear usuario

  POST /api/Auth/login
  Content-Type: application/json

  {
    "username": "string",
    "password": "string"
  }

- Registrar usuarios

  POST /api/Auth/register
  Content-Type: application/json

  {
    "username": "string",
    "password": "string",
    "roles": [
      "Admin",
      "User"
    ]
  }

### Empleado

La API expone varios endpoints para gestionar empleados, departamentos, proyectos y más. 
Es válido recalcar que la API está protegida por autenticación Token (Bearer <Your API key>).

Aquí tienes algunos ejemplos de cómo interactuar con la API:

- Obtener todos los empleados

  GET /api/employees

- Crear un nuevo empleado

  Los IDs para `departmentId` y `projectId` están relacionados con la parte 4. Script SQL. 
  Para `currentPosition`, existe un enum y sus respectivos valores son (0 = RegularEmployee, 1 = Manager, 2 = SeniorManager).

  POST /api/employees
  Content-Type: application/json

  {
    "id": 0,
    "document": 0,
    "name": "string",
    "currentPosition": 0,
    "salary": 0,
    "departmentId": 0,
    "projectId": 0
  }

- Actualizar un empleado existente

  PUT /api/employees/1
  Content-Type: application/json

  {
    "id": 0,
    "document": 0,
    "name": "string",
    "currentPosition": 0,
    "salary": 0,
    "departmentId": 0,
    "projectId": 0
  }

- Eliminar un empleado

  DELETE /api/employees/1
 
 