using Microsoft.AspNetCore.Mvc;
using MVC_Productos.Models;
using MVC_Productos.Services;

namespace MVC_Productos.Controllers.Api
{
    /// <summary>
    /// Controlador API para gestionar las operaciones relacionadas con los productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoApiController : ControllerBase
    {
        private readonly ProductoService _productoService;

        /// <summary>
        /// Constructor que inyecta el servicio de productos.
        /// </summary>
        /// <param name="productoService">Servicio para manejar la lógica de negocio de productos.</param>
        public ProductoApiController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ObtenerProductos()
        {
            var productos = await _productoService.ObtenerTodosAsync();
            return Ok(productos); // Devuelve un array vacío si es null
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);
            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }
            return Ok(producto);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            var nuevoProducto = await _productoService.CrearAsync(producto);
            return CreatedAtAction(nameof(ObtenerProducto), new { id = nuevoProducto.Id }, nuevoProducto);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest("El ID del producto no coincide.");
            }

            var actualizado = await _productoService.ActualizarAsync(producto);
            if (!actualizado)
            {
                return NotFound("Producto no encontrado.");
            }

            return NoContent();
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var eliminado = await _productoService.EliminarAsync(id);
            if (!eliminado)
            {
                return NotFound("Producto no encontrado.");
            }

            return NoContent();
        }
    }
}