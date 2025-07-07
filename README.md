# Student Management API (.NET 8 - Clean Architecture & DDD)

Esta es la API que respalda el sistema de gestión de estudiantes. Fue desarrollada con .NET 8 siguiendo los principios de **Clean Architecture**, **Domain-Driven Design (DDD)** y buenas prácticas de separación de responsabilidades.

---

## Funcionalidades principales

-  Autenticación con JWT
-  Gestión de estudiantes (Logica Dominio)
-  Gestión de materias (Logica Dominio)
-  Gestión de profesores
-  Arquitectura basada en **Domain-Driven Design (DDD)**

---

## Estructura del Proyecto

StudentManagementApi.sln
│
├── Application/ → Lógica de negocio 
│ ├── DTOs/ → Data Transfer Objects
│ ├── Interfaces/ → Contratos de servicios y repositorios
│ └── Services/ → Implementaciones de lógica de aplicación
│
├── Domain/ → Núcleo de dominio puro
│ ├── Entities/ → Entidades del dominio
│ ├── Exceptions/ → Excepciones de negocio
│ ├── Interface/ → Interfaces específicas del dominio
│ └── ValueObjects/ → Objetos de valor
│
├── Infrastructure/ → Capa de infraestructura
│ ├── Auth/ → Servicios de autenticación y JWT
│ ├── Repository/ → Repositorios y acceso a procedimientos almacenados
│ ├── Serialization/ → Configuración de serialización
│ └── Service/ → Implementaciones de servicios compartidos
│
└── StudentManagementApi/ → Capa de presentación (API)
├── Controllers/ → Controladores 
├── Middleware/ → Middlewares personalizados
├── appsettings.json → Configuración de entorno
└── Program.cs → Bootstrap principal de la aplicación

##  Cómo ejecutar localmente

git clone https://github.com/tu-usuario/student-management-api.git
cd student-management-api
dotnet run --project StudentManagementApi
http://localhost:5000/swagger

