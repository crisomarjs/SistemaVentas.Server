using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> Lista();
    }
}
