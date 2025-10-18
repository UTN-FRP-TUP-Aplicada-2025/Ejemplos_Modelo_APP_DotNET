using Ejemplo_Console_Host.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;


using var host = Host.CreateDefaultBuilder(args)
.ConfigureAppConfiguration((context, config) =>
{
    // limpia proveedores previos (opcional)
    config.Sources.Clear();

    // cargar configuración desde appsettings.json
    config.SetBasePath(Directory.GetCurrentDirectory());
    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
})
.ConfigureServices((context, services) =>
{
    // registrar dependencias
    services.AddTransient<IUsuarioRepository, UsuarioRepository>();
    services.AddScoped<IUsuarioService, UsuarioService>();
})
.Build();


// crear un alcance (scope) para servicios Scoped
using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

// resuelve el servicio y usarlo
var usuarioService = services.GetRequiredService<IUsuarioService>();
usuarioService.MostrarUsuario(7);
