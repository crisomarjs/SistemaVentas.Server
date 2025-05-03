namespace SistemaVentas.Server.Repository
{
    public interface IDashBoardRepository
    {
        Task<int> TotalVentasUltimaSemana();
        Task<string> TotalIngresosUltimaSemana();
        Task<int> TotalProductos();
        Task<Dictionary<string, int>> VentasUltimaSemana(); 
    }
}
