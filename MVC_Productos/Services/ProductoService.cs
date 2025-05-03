using Microsoft.EntityFrameworkCore;
using MVC_Productos.Data;
using MVC_Productos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Productos.Services
{
    /// <summary>
    /// Servicio para manejar la lógica de negocio de los productos.
    /// </summary>
    public class ProductoService
    {
        private readonly DataContex _context;

        public ProductoService(DataContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Producto> CrearAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> ActualizarAsync(Producto producto)
        {
            if (!_context.Productos.Any(p => p.Id == producto.Id))
            {
                return false;
            }

            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return false;
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}