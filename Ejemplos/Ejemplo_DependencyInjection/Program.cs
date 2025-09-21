using Ejemplo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string miParametro = configuration["MiParametro"];

var services = new ServiceCollection();
services.AddTransient<FormPrincipal>();
var serviceProvider = services.BuildServiceProvider();

ApplicationConfiguration.Initialize();
var form = serviceProvider.GetRequiredService<FormPrincipal>();
MessageBox.Show($"MiParametro: {miParametro}");
Application.Run(form);
