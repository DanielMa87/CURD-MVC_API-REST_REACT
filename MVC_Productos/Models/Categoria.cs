using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Productos.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // Relación con Producto
        [InverseProperty("Categoria")]
        public List<Producto> ListaProdutos { get; set; } = new();
    }
}