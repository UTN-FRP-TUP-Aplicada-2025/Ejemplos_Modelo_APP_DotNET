using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejemplo_Console_Host.Models;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repo;
    private readonly IConfiguration _config;

    // Inyección de dependencias: repositorio + configuración
    public UsuarioService(IUsuarioRepository repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    public void MostrarUsuario(int id)
    {
        var appName = _config["AppSettings:NombreAplicacion"];
        var version = _config["AppSettings:Version"];

        var usuario = _repo.ObtenerUsuario(id);

        Console.WriteLine($"[{appName} v{version}] {usuario}");
    }
}

