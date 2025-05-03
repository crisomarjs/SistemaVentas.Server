using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository.Implementation
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbsistemaVentasContext _context;
        public UsuarioRepository(DbsistemaVentasContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<Usuario>> Consultar(Expression<Func<Usuario, bool>> filtro = null)
        {
            IQueryable<Usuario> queryEntidad = filtro == null ? _context.Usuarios : _context.Usuarios.Where(filtro);
            return queryEntidad;
        }

        public async Task<Usuario> Crear(Usuario usuario)
        {
            try
            {
                _context.Set<Usuario>().Add(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(Usuario usuario)
        {
            try
            {
                _context.Set<Usuario>().Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(Usuario usuario)
        {
            try
            {
                _context.Set<Usuario>().Remove(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Usuario>> Lista()
        {
            try
            {
                return await _context.Usuarios.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Usuario> Obtener(Expression<Func<Usuario, bool>> filtro = null)
        {
            try
            {
                return await _context.Usuarios.Where(filtro).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
