# 🛠️ Sistema de Ventas - API REST (.NET 8)

Este es el backend del **Sistema de Ventas**, desarrollado con **ASP.NET Core Web API en .NET 8**. Expone endpoints REST que permiten gestionar productos, categorías, ventas, usuarios y más.

🔗 Repositorio del cliente Blazor WebAssembly:  
https://github.com/crisomarjs/SistemaVentas.Client

---

## 🧱 Estructura del Proyecto
- 📁 Controllers -> Controladores HTTP para exponer endpoints
- 📁 DTOs -> Modelos para la transferencia de datos entre capas
- 📁 Models -> Entidades del dominio y modelos de base de datos
- 📁 Properties -> Archivos de configuración del proyecto
- 📁 Repository -> Interfaces y clases concretas para acceso a datos
- 📁 Utilities -> Clases auxiliares (por ejemplo, para validaciones, tokens, etc.)


---

## 🚀 Tecnologías Utilizadas

- .NET 8 + ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- CORS habilitado para el cliente Blazor WebAssembly

---

## 🔌 Cómo Ejecutarlo

1. Clona el repositorio
2. Asegúrate de tener configurada la cadena de conexión en appsettings.json o appsettings.Development.json:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Tu cadena de conexion a la base de datos"
}
```
3. Ejecuta el proyecto: dotnet run

---
## 📸 Capturas de Pantalla

![image](https://github.com/user-attachments/assets/e0fc4d81-838a-4cb9-950d-3f644f5fb9d3)
![image](https://github.com/user-attachments/assets/e198d7da-013b-466b-a13d-359face49793)


