

namespace Console_Host.Models;

public class UsuarioRepository : IUsuarioRepository
{
    public string ObtenerUsuario(int id) => $"Usuario con ID {id}";
}

