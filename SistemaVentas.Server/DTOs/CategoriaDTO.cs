namespace SistemaVentas.Server.DTOs
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }

        public override bool Equals(object o)
        {
            var other = o as CategoriaDTO;
            return other?.IdCategoria == IdCategoria;
        }
        public override int GetHashCode() => IdCategoria.GetHashCode();
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
