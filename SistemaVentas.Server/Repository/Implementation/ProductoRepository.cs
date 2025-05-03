using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository.Implementation
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DbsistemaVentasContext _context;
        public ProductoRepository(DbsistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Producto>> Consultar(Expression<Func<Producto, bool>> filtro = null)
        {
            IQueryable<Producto> queryEntidad = filtro != null ? _context.Productos : _context.Productos.Where(filtro);
            return queryEntidad;
        }

        public async Task<Producto> Crear(Producto producto)
        {
            try
            {
                _context.Set<Producto>().Add(producto);
                await _context.SaveChangesAsync();
                return producto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(Producto producto)
        {
            try
            {
                _context.Set<Producto>().Update(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(Producto producto)
        {
            try
            {
                _context.Set<Producto>().Remove(producto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Producto> Obtener(Expression<Func<Producto, bool>> filtro = null)
        {
            try
            {
                return await _context.Productos.Where(filtro).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
