using System.Linq.Expressions;
using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> Lista();
        Task<Usuario> Obtener(Expression<Func<Usuario, bool>> filtro = null);
        Task<Usuario> Crear(Usuario usuario);
        Task<bool> Editar(Usuario usuario);
        Task<bool> Eliminar(Usuario usuario);
        Task<IQueryable<Usuario>> Consultar(Expression<Func<Usuario, bool>> filtro = null);

    }
}
