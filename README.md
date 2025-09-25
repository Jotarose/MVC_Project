# Amigos

**Amigos** es una aplicación web ASP.NET Core MVC para gestionar una base de datos de amigos, con soporte multilenguaje (español e inglés) y almacenamiento en SQLite. Permite crear, editar, eliminar y filtrar amigos por distancia geográfica.

## Tabla de Contenidos

- [Características](#características)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Instalación](#instalación)
- [Configuración](#configuración)
- [Uso](#uso)
- [Internacionalización](#internacionalización)
- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Licencias de Dependencias](#licencias-de-dependencias)
- [Autores](#autores)

---

## Características

- CRUD completo de amigos (nombre, longitud, latitud)
- Filtro de amigos por distancia geográfica
- Interfaz multilenguaje (español/inglés)
- Persistencia con Entity Framework Core y SQLite
- Validación de formularios en cliente y servidor
- Diseño responsivo con Bootstrap

## Estructura del Proyecto

```bash
MVC_Project/
├── Amigos.sln
└── Amigos/
    ├── Amigos.csproj
    ├── Amigos.db
    ├── appsettings.json
    ├── appsettings.Development.json
    ├── Program.cs
    ├── Controllers/
    │   └── AmigoController.cs
    ├── DataAccessLayer/
    │   └── AmigoDBContext.cs
    ├── Models/
    │   └── Amigo.cs
    ├── Migrations/
    ├── Material/
    │   ├── Controllers/
    │   │   ├── AmigoController.es.resx
    │   │   └── AmigoController.en.resx
    │   └── Views/
    │       └── Amigo/
    │           ├── *.es.resx
    │           └── *.en.resx
    ├── Views/
    │   ├── Amigo/
    │   │   ├── Index.cshtml
    │   │   ├── Create.cshtml
    │   │   ├── Edit.cshtml
    │   │   ├── Delete.cshtml
    │   │   └── Details.cshtml
    │   └── Shared/
    │       ├── _Layout.cshtml
    │       ├── _Idioma.cshtml
    │       ├── _ValidationScriptsPartial.cshtml
    │       └── Error.cshtml
    ├── wwwroot/
    │   ├── css/
    │   ├── js/
    │   └── lib/
    └── Properties/
        └── launchSettings.json
````

## Instalación

1. Clona el repositorio: git clone <url-del-repositorio> cd Amigos/Amigos
2. Restaura los paquetes NuGet: dotnet restore
3. Aplica las migraciones y crea la base de datos: dotnet ef database update
4. Ejecuta la aplicación: dotnet run

La aplicación estará disponible en http://localhost:5088 (o el puerto configurado).

## Configuración

- La cadena de conexión a la base de datos se encuentra en `appsettings.json`.
- Los idiomas soportados se configuran en `Program.cs` y los archivos de recursos `.resx` están en la carpeta `Material`.

## Uso

- Accede a la página principal y navega a la sección "Amigos".
- Utiliza el formulario para filtrar amigos por distancia, longitud y latitud.
- Cambia el idioma desde el menú superior.
- Crea, edita, visualiza o elimina amigos desde la tabla principal.

## Internacionalización

- Los textos de la interfaz están en archivos .resx dentro de Material/Controllers y Material/Views/Amigo.
- Para añadir un nuevo idioma, crea los archivos .resx correspondientes y actualiza la configuración en Program.cs.

## Tecnologías utilizadas

- ASP.NET Core MVC
- Entity Framework Core (SQLite)
- Bootstrap 5
- jQuery y jQuery Validation

## Licencias de Dependencias

- Bootstrap - MIT
- jQuery - MIT
- jQuery Validation - MIT
- jQuery Validation Unobtrusive - Apache 2.0
