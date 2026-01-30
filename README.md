# Website Backend Template

A ready-to-use **ASP.NET Core MVC backend template** for content‑driven websites. This project provides a structured foundation for managing pages, assets, catalogs, categories, and related metadata, with a clean separation of concerns and common backend services wired in.

## ✨ Features

- ASP.NET Core MVC architecture
- Entity Framework Core data access
- Modular controllers for content, assets, catalogs, and pages
- ViewModels for clean request/response handling
- Background services for maintenance tasks
- Environment‑specific configuration (Development / Staging / Production)
- SMTP email support
- Asset and media management helpers
- Ready for extension into a full CMS or headless backend

## 🛠️ Tech Stack

- **.NET / ASP.NET Core**
- **MVC Pattern** (Controllers, Views, ViewModels)
- **Entity Framework Core**
- **C#**
- **LibMan** (client‑side library management)

## 📂 Project Structure

```
website-backend-template-main/
├── Controllers/        # MVC controllers (Pages, Assets, Catalogs, Users, etc.)
├── Data/               # EF Core DbContext and seed data
├── Models/             # Domain models
├── ViewModels/         # View-specific data transfer models
├── Services/           # Reusable services and background workers
├── Areas/              # Feature-based MVC areas
├── Views/              # Razor views
├── wwwroot/            # Static files
├── Properties/         # Launch and service dependency settings
├── appsettings.*.json  # Environment-specific configuration
├── Program.cs          # Application entry point
```

## 🚀 Getting Started

### Setup
- Is managed by [ZigiProjectManager ](https://github.com/Saravasha/ZigiProjectManger)

The application uses environment-based configuration:

- `appsettings.Development.json`
- `appsettings.Staging.json`
- `appsettings.Production.json`

Common configurable sections include:

- Database connection strings
- SMTP email settings
- Logging levels

## 📌 Use Cases

- Website backend / CMS foundation
- Headless API extension
- Admin dashboard backend
- Content and asset management systems

## 📄 License

This project is provided as a template. Add your preferred license before distributing or deploying.

---

**Happy building!** 🚀