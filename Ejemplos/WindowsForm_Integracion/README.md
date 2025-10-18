# Windows Forms con .NET 8 - Guía de Implementación

## 1. Programa Implícito (Top-level statements)

En .NET 8, podemos simplificar nuestro programa eliminando el código explícito del `Program.cs`:

```csharp
// Antes
namespace Ejemplo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}

// Después
using Ejemplo;

ApplicationConfiguration.Initialize();
Application.Run(new Form1());
```

## 2. Inyección de Dependencias

### Instalación
```bash
dotnet add package Microsoft.Extensions.DependencyInjection
```

### Implementación
```csharp
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTransient<Form1>();
var serviceProvider = services.BuildServiceProvider();

var form = serviceProvider.GetRequiredService<Form1>();
Application.Run(form);
```

### Tipos de Registros de Servicios

1. **Transient** (`services.AddTransient<T>()`)
   - Crea una nueva instancia cada vez que se solicita
   - Útil para: servicios ligeros y sin estado
   - Ejemplo:
   ```csharp
   services.AddTransient<IDataValidator, DataValidator>();
   ```

2. **Scoped** (`services.AddScoped<T>()`)
   - Una instancia por scope (en Windows Forms, típicamente por ventana)
   - Útil para: servicios que deben mantener estado durante una operación
   - Ejemplo:
   ```csharp
   services.AddScoped<IUserSession, UserSession>();
   ```

3. **Singleton** (`services.AddSingleton<T>()`)
   - Una única instancia para toda la aplicación
   - Útil para: servicios que deben mantener estado global
   - Ejemplo:
   ```csharp
   services.AddSingleton<IConfiguration, Configuration>();
   ```

### Ejemplos de Registro de Servicios
```csharp
// Registrar servicios
services.AddTransient<IProductService, ProductService>();
services.AddScoped<IOrderProcessor, OrderProcessor>();
services.AddSingleton<IGlobalSettings, GlobalSettings>();

// Registrar formularios
services.AddTransient<MainForm>();
services.AddTransient<ProductForm>();
services.AddTransient<SettingsForm>();
```

## 3. Configuración (appsettings.json)

### Paquetes Necesarios
```bash
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
```

Este sistema forma parte del patrón de **Options Pattern** de Microsoft, que es una práctica recomendada para manejar configuraciones en aplicaciones .NET.

### Estructura de appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=myServerAddress;Database=myDatabase;"
  },
  "AppSettings": {
    "Title": "Mi Aplicación",
    "Theme": "Dark"
  },
  "Feature": {
    "IsEnabled": true,
    "MaxItems": 100
  }
}
```

### Lectura de Configuración
```csharp
// Configuración básica
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Lectura directa
string title = configuration["AppSettings:Title"];
bool isEnabled = bool.Parse(configuration["Feature:IsEnabled"]);

// Usando clases tipadas (recomendado)
public class AppSettings
{
    public string Title { get; set; }
    public string Theme { get; set; }
}

public class FeatureSettings
{
    public bool IsEnabled { get; set; }
    public int MaxItems { get; set; }
}

// Registro en el contenedor
services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
services.Configure<FeatureSettings>(configuration.GetSection("Feature"));

// Uso en una clase
public class MyService
{
    private readonly AppSettings _settings;

    public MyService(IOptions<AppSettings> settings)
    {
        _settings = settings.Value;
    }
}
```

## Arquitectura Clean
Todo lo anterior forma parte del patrón de arquitectura moderna de Microsoft conocido como "Clean Architecture" o "Onion Architecture", donde:

1. La Inyección de Dependencias permite desacoplar componentes
2. El Options Pattern permite externalizar la configuración
3. Los servicios pueden ser fácilmente reemplazados o mockeados para testing

Esta estructura facilita:
- Mantenibilidad
- Testabilidad
- Separación de responsabilidades
- Configuración flexible