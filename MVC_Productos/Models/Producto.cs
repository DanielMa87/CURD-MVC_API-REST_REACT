namespace MVC_Productos.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        // Relación con Categoria
        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        [InverseProperty("ListaProdutos")]
        public Categoria? Categoria { get; set; }

        // Relación con Proveedor
        [ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }
        [InverseProperty("ListaProdutos")]
        public Proveedor? Proveedor { get; set; }
    }
}
