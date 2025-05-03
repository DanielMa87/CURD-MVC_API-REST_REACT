using Microsoft.EntityFrameworkCore;
using MVC_Productos.Models;

namespace MVC_Productos.Data
{
    public class DataContex:DbContext
    {
        public DataContex(DbContextOptions<DataContex>options): base(options)//constructor
        {

        }

        public DbSet<Models.Producto> Productos { get;set;}//set de datos de tipo producto que en la bd se vera reflejada en una tabla productos
        public DbSet<Models.Proveedor> Proveedores { get;set;}
        public DbSet<Models.Categoria> Categorias { get;set;}
    }
}