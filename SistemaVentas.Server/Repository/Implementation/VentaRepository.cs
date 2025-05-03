using System.Globalization;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository.Implementation
{
    public class VentaRepository : IVentaRepository
    {
        private readonly DbsistemaVentasContext _context;
        public VentaRepository(DbsistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<List<Venta>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = _context.Venta;

            if (buscarPor == "fecha")
            {

                DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
                DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));

                return query.Where(v =>
                    v.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                    v.FechaRegistro.Value.Date <= fech_Fin.Date
                )
                .Include(dv => dv.DetalleVenta)
                .ThenInclude(p => p.IdProductoNavigation)
                .ToList();

            }
            else
            {
                return query.Where(v => v.NumeroDocumento == numeroVenta)
                  .Include(dv => dv.DetalleVenta)
                  .ThenInclude(p => p.IdProductoNavigation)
                  .ToList();
            }
        }

        public async Task<Venta> Registrar(Venta venta)
        {
            Venta VentaGenerada = new Venta();

            //usaremos transacion, ya que si ocurre un error en algun insert a una tabla, debe reestablecer todo a cero, como si no hubo o no existió ningun insert
            using (var transaction = _context.Database.BeginTransaction())
            {
                int CantidadDigitos = 4;
                try
                {
                    foreach (DetalleVenta dv in venta.DetalleVenta)
                    {
                        Producto producto_encontrado = _context.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;
                        _context.Productos.Update(producto_encontrado);
                    }
                    await _context.SaveChangesAsync();


                    NumeroDocumento correlativo = _context.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _context.NumeroDocumentos.Update(correlativo);
                    await _context.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    venta.NumeroDocumento = numeroVenta;

                    await _context.Venta.AddAsync(venta);
                    await _context.SaveChangesAsync();

                    VentaGenerada = venta;

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return VentaGenerada;
        }

        public async Task<List<DetalleVenta>> Reporte(string FechaInicio, string FechaFin)
        {
            DateTime fech_Inicio = DateTime.ParseExact(FechaInicio, "dd/MM/yyyy", new CultureInfo("es-MX"));
            DateTime fech_Fin = DateTime.ParseExact(FechaFin, "dd/MM/yyyy", new CultureInfo("es-MX"));

            List<DetalleVenta> listaResumen = await _context.DetalleVenta
                .Include(p => p.IdProductoNavigation)
                .Include(v => v.IdVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Inicio.Date && dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date)
                .ToListAsync();

            return listaResumen;
        }
    }
}
