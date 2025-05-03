using Microsoft.EntityFrameworkCore;
using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository.Implementation
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly DbsistemaVentasContext _context;
        public CategoriaRepository(DbsistemaVentasContext context)
        {
            _context = context;
        }
        public async Task<List<Categoria>> Lista()
        {
            try
            {
                return await _context.Categoria.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
