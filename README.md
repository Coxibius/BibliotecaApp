# 📚 BibliotecaApp

Aplicación web de gestión de biblioteca escolar desarrollada con **ASP.NET Core 9 MVC** y **Entity Framework Core**. Permite administrar autores, libros, estudiantes y préstamos, con una página de inicio que muestra el catálogo de libros disponibles.

---

## 🛠️ Stack Tecnológico

| Capa | Tecnología |
|---|---|
| Framework | ASP.NET Core 9 MVC |
| ORM | Entity Framework Core 9 |
| Base de datos | SQL Server Express |
| UI | Bootstrap 5 + Bootstrap Icons |
| Lenguaje | C# 13 / .NET 9 |

---

## 📁 Estructura del Proyecto

```
BibliotecaApp/
├── Controllers/
│   ├── HomeController.cs        # Página de inicio (catálogo)
│   ├── AutoresController.cs     # CRUD de autores
│   ├── LibrosController.cs      # CRUD de libros
│   ├── EstudiantesController.cs # CRUD de estudiantes
│   └── PrestamosController.cs   # CRUD de préstamos + devolución
├── Models/
│   ├── Autor.cs
│   ├── Libro.cs
│   ├── Estudiante.cs
│   └── Prestamo.cs
├── Data/
│   └── BibliotecaContext.cs     # DbContext de EF Core
├── Views/                       # Vistas Razor por controlador
├── Migrations/                  # Migraciones de EF Core
└── appsettings.json             # Configuración (cadena de conexión)
```

---

## 🗄️ Modelos y Relaciones

```
Autor  1 ── N  Libro  1 ── N  Prestamo  N ── 1  Estudiante
```

### Autor
| Campo | Tipo |
|---|---|
| Id | int (PK) |
| Nombre | string? |
| Nacionalidad | string? |
| FechaNacimiento | DateTime |

### Libro
| Campo | Tipo |
|---|---|
| Id | int (PK) |
| Titulo | string? |
| ISBN | string? |
| AnioPublicacion | int |
| Disponible | bool (default: true) |
| AutorId | int (FK → Autor) |

### Estudiante
| Campo | Tipo |
|---|---|
| Id | int (PK) |
| Nombre | string? |
| Matricula | string? |

### Prestamo
| Campo | Tipo |
|---|---|
| Id | int (PK) |
| FechaPrestamo | DateTime (default: now) |
| FechaDevolucion | DateTime? |
| LibroId | int (FK → Libro) |
| EstudianteId | int (FK → Estudiante) |

---

## ⚙️ Requisitos Previos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server Express (o SQL Server completo)
- Visual Studio 2022 / VS Code (opcional)

---

## 🚀 Instalación y Puesta en Marcha

### 1. Clonar el repositorio

```bash
git clone <url-del-repo>
cd act-8/BibliotecaApp
```

### 2. Configurar la cadena de conexión

Edita `appsettings.json` y ajusta el servidor si es necesario:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=BibliotecaDb;Integrated Security=True;TrustServerCertificate=True;"
}
```

### 3. Aplicar migraciones y crear la base de datos

```bash
dotnet ef database update
```

### 4. Ejecutar la aplicación

```bash
dotnet run
```

La aplicación estará disponible en `https://localhost:5001` (o el puerto que indique la consola).

---

## 🔑 Funcionalidades Principales

- **Catálogo público** — La página de inicio muestra todos los libros con estado `Disponible = true`, con el nombre del autor y el año de publicación.
- **CRUD de Autores** — Crear, ver, editar y eliminar autores.
- **CRUD de Libros** — Crear, ver, editar y eliminar libros, asociados a un autor.
- **CRUD de Estudiantes** — Registrar y gestionar estudiantes.
- **Préstamos** — Crear préstamos (solo libros disponibles); al crear un préstamo el libro pasa a `Disponible = false`.
- **Devolución** — Acción `Devolver` que registra `FechaDevolucion` y devuelve el libro a `Disponible = true`.
- **Eliminación de préstamo** — Si el préstamo no fue devuelto, restaura automáticamente la disponibilidad del libro.

---

## 📝 Comandos Útiles de EF Core

```bash
# Crear una nueva migración
dotnet ef migrations add <NombreMigracion>

# Aplicar migraciones pendientes
dotnet ef database update

# Revertir la última migración
dotnet ef migrations remove
```
