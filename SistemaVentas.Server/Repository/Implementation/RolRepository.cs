using Microsoft.EntityFrameworkCore;
using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository.Implementation
{
    public class RolRepository : IRolRepository
    {
        private readonly DbsistemaVentasContext _context;
        public RolRepository(DbsistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> Lista()
        {
            try
            {
                return await _context.Rols.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
