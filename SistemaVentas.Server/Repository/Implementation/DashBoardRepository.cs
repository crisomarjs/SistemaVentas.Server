
using System.Globalization;
using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository.Implementation
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly DbsistemaVentasContext _context;
        public DashBoardRepository(DbsistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            try
            {
                IQueryable<Venta> _ventaQuery = _context.Venta.AsQueryable();
                if(_ventaQuery.Count() > 0)
                {
                    DateTime? ultimaFecha = _context.Venta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
                    ultimaFecha = ultimaFecha.Value.AddDays(-7);
                    IQueryable<Venta> query = _context.Venta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);

                    resultado = query
                        .Select(v => v.Total)
                        .Sum(v => v.Value);
                }

                return Convert.ToString(resultado, new CultureInfo("es-MX"));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> TotalProductos()
        {
            try
            {
                IQueryable<Producto> query = _context.Productos;
                int total = query.Count();
                return total;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            try
            {
                IQueryable<Venta> _ventaQuery = _context.Venta.AsQueryable();
                if (_ventaQuery.Count() > 0)
                {
                    DateTime? ultimaFecha = _context.Venta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
                    ultimaFecha = ultimaFecha.Value.AddDays(-7);

                    IQueryable<Venta> query = _context.Venta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
                    total = query.Count();
                }

                return total;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();
            try
            {
                IQueryable<Venta> _ventaQuery = _context.Venta.AsQueryable();
                if(_ventaQuery.Count() > 0)
                {
                    DateTime? ultimaFecha = _context.Venta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
                    ultimaFecha = ultimaFecha.Value.AddDays(-7);
                    IQueryable<Venta> query = _context.Venta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
                    
                    resultado = query
                        .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(v => v.Key)
                        .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                        .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
                }

                return resultado;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
