# TalentoLocal

Backend API para la aplicación TalentoLocal (Gestión de convocatorias y ofertas).

- Proyecto: ASP.NET Core Web API (.NET 8)
- ORM: Entity Framework Core (DbContext, Migrations)
- Arquitectura: Controllers → Services → Repositories, con DTOs y Mappers
- Base de datos: SQL Server (producción) / InMemory (desarrollo)

Frontend repo: https://github.com/Akiii-lab/Talento-Local-Web-Client/

Estructura principal
- `Controllers/` — endpoints HTTP (Offers, OfferCategory, Postulation, Evaluation, Favorites, etc.)
- `Services/` — lógica de negocio y validaciones
- `Repositories/` — acceso a datos (EF Core)
- `Models/` — entidades EF Core y `DbDevopsContext`
- `DTOs/` — objetos de transferencia (requests/responses)
- `Mappers/` — conversión entidad ⇄ DTO
- `Migrations/` — migraciones EF Core generadas
- `Program.cs` — configuración DI, serialización y proveedor de base de datos

Endpoints de interés
- `GET /api/offers` — listar ofertas
- `GET /api/offers/{id}` — obtener oferta por id
- `GET /api/offers/search-by-category?category=<nombre>` — buscar ofertas por categoría (usa query string)
- `POST /api/offers` — crear oferta
- `PUT /api/offers/{id}` — actualizar oferta
- `DELETE /api/offers/{id}` — eliminar oferta

Configuración y secretos

- Usa `appsettings.json` para valores no sensibles y variables de entorno o Azure Key Vault para secretos.

Construir y ejecutar (PowerShell)
```powershell
dotnet build TalentoLocal.csproj
dotnet run --project TalentoLocal.csproj
# La API se expondrá en el puerto/URL configurado en Properties/launchSettings.json
```

Contacto
- Proyecto mantenido localmente por el equipo de TalentoLocal.

---

TalentoLocal © 2025. Todos los derechos reservados.

Carlos Romero,
Carlos Lizarazo,
Javier Santodomingo, 
Cristina Sanchez.
