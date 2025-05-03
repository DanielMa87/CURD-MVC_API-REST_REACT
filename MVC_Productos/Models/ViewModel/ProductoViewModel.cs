using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Plugins;

namespace MVC_Productos.Models.ProductoViewModel

{
    public class ProductoViewModel
    {
        public List<Models.Producto> Productos {get; set;} = new List<Models.Producto>();//inicializo lista de productos vacia
        public string BuscarTexto{get; set;} = string.Empty;
    }
}