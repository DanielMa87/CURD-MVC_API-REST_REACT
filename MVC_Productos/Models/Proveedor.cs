using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Productos.Models
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Relación con Producto
        [InverseProperty("Proveedor")]
        public List<Producto> ListaProdutos { get; set; } = new();
    }
}