using System.Linq.Expressions;
using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository
{
    public interface IProductoRepository
    {
        Task<Producto> Obtener(Expression<Func<Producto, bool>> filtro = null);
        Task<Producto> Crear(Producto producto);
        Task<bool> Editar(Producto producto);
        Task<bool> Eliminar(Producto producto);
        Task<IQueryable<Producto>> Consultar(Expression<Func<Producto, bool>> filtro = null);   
    }
}
