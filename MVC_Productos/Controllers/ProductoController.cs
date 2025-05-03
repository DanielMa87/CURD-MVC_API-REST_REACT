
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_Productos.Data;
using MVC_Productos.Models;
using MVC_Productos.Models.ProductoViewModel;

namespace MVC_Productos.Controllers
{
       public class ProductoController : Controller
    {
        private readonly DataContex _context;

        public ProductoController(DataContex context){
            _context = context;
        }

        public IActionResult Index(string BuscarTexto)
        {
            
           
            //var producto = new Models.Producto{Id=1, Nombre="Desodorante", Precio= 10};//cargo un producto 
            var productos = _context.Productos.Include(c=>c.Categoria).Include(p =>p.Proveedor).ToList();//con los include agrego los atributos foraneos
            if(!string.IsNullOrEmpty(BuscarTexto)){//si recibo un string de busqueda
                productos =productos.Where(p=>p.Nombre.Contains(BuscarTexto)).ToList();
             } 
            
            var ProductoViewModel=new ProductoViewModel();
            
            ProductoViewModel.Productos=productos;
            ProductoViewModel.BuscarTexto=BuscarTexto;
            
            return View(ProductoViewModel);// retorno
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id){
            if(id==null)
            {
                return NotFound();
            }
            var producto =await _context.Productos//accedo a la tabla productos, con await espera en forma asincronica que se complete la operacion
            .Include(p=>p.Categoria)//incluyo los datos de la categoria
            .Include(p=>p.Proveedor)//incluyo los datos del poveedor
            .FirstOrDefaultAsync(p=>p.Id == id);//busco el primer producto que coincida el id
            if (producto==null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpGet]//indicamos que es un metodod de tipo get (a solicitud desde el navegador)
        public async Task<IActionResult> Create(){
            //mientras espera recuperar listas, por eso debe ser asincronica
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            ViewBag.Proveedores = await _context.Proveedores.ToListAsync();

            return View();

        }

        [HttpPost]//indicamos que es un metodod de tipo post (a recibe in desde el navegador)
        //async Task permite trabajar de manea asincronica, realiza la operacion en segundo plano y devuelve el objeto task,
        //con la palabra await se esepera el resultado de la tarea (evitamos que subproceso bloquee durante opreaciones que toman tiempo)
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,CategoriaId,ProveedorId")]Producto producto)
        {
            if(ModelState.IsValid){
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));//retorna a la vista index

            }
            
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            ViewBag.Proveedores = await _context.Proveedores.ToListAsync();
            return View(producto);//si no es valido regreso el producto para editarlo por ejemplo

        }

       [HttpGet]
       public async Task<IActionResult> Edit(int? id)
       {
            if (id==null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            ViewBag.Proveedores = await _context.Proveedores.ToListAsync();
            return View(producto);
        }

        [HttpPost]
       public async Task<IActionResult> Edit(int id,[Bind("Id,Nombre,Descripcion,Precio,CategoriaId,ProveedorId")]Producto producto)
       { 
            if (id!=producto.Id)
            {
                return BadRequest();
            }
             //var productoDb = await _context.Productos.FindAsync(id);
             if(ModelState.IsValid)
             {
                _context.Update(producto);
                await _context.SaveChangesAsync();//confirmando los cambios
                return RedirectToAction(nameof(Index));//retorna a la vista index
             }
            
             ViewBag.Categorias = await _context.Categorias.ToListAsync();
             ViewBag.Proveedores = await _context.Proveedores.ToListAsync();
             return View(producto);
       }
       [HttpGet]
       public async Task<IActionResult> Delete(int? id)
       {
        
        if(id==null)
        {
            return NotFound();
        }
        
        var producto = await _context.Productos.FindAsync(id);
        
        if(producto==null){
             return NotFound(); 
        }

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));//retorna a la vista index
       }
    
    }
    

}