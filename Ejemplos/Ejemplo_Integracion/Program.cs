using Ejemplo_Integracion;
using Ejemplo_Integracion.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

//string connectionString=configuration.GetSection("ConnectionStrings:DefaultConnection").Value;


//registro de las dependencias
var services = new ServiceCollection();


//con Microsoft.Extensions.Options y  Microsoft.Extensions.Options.ConfigurationExtensions;
services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
services.AddSingleton<ProductosRepository>();

////opción alternativa
//string connectionString = configuration.GetConnectionString("DefaultConnection");
//services.AddSingleton<ProductosRepository>(provider =>
//{
//    var connStrings = new ConnectionStrings { DefaultConnection = connectionString };
//    return new ProductosRepository(Microsoft.Extensions.Options.Options.Create(connStrings));
//});


//sin Microsoft.Extensions.Options
//services.AddSingleton<ProductosRepository>(p => new ProductosRepository(connectionString) );


services.AddTransient<Form1>();

//se instancia el contenedor que administra el ciclo de vida de las dependencias.
var serviceProvider = services.BuildServiceProvider();

var form = serviceProvider.GetRequiredService<Form1>();
Application.Run(form);
