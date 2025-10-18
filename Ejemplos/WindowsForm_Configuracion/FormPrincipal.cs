using Microsoft.Extensions.Configuration;

namespace WindowsForm_Configuracion;

public partial class FormPrincipal : Form
{
    public FormPrincipal()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        // Lectura directa
        string title = configuration["AppSettings:Title"];
        bool isEnabled = bool.Parse(configuration["Feature:IsEnabled"]);

        // Registro en el contenedor
        var r=configuration.GetSection("AppSettings");
        var f=configuration.GetSection("Feature");
    }

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
}
