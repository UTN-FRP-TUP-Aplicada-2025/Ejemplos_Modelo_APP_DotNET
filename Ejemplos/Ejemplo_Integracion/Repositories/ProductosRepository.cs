using Microsoft.Extensions.Options;

namespace Ejemplo_Integracion.Repositories;

public class ProductosRepository
{
    private readonly string _connectionString;

    public ProductosRepository(IOptions<ConnectionStrings> options)
    {
        _connectionString = options.Value.DefaultConnection;
    }
}
