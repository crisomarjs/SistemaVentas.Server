using SistemaVentas.Server.Models;

namespace SistemaVentas.Server.Repository
{
    public interface IVentaRepository
    {
        Task<Venta> Registrar(Venta venta);
        Task<List<Venta>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);
        Task<List<DetalleVenta>> Reporte(string FechaInicio, string FechaFin);
    }
}
