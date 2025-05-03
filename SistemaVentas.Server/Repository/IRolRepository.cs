using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository
{
    public interface IRolRepository
    {
        Task<List<Rol>> Lista();
    }
}
