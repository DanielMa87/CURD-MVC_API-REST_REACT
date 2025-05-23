

 # Proyecto DemoMvc: CRUD MVC y API REST con Swagger

Este proyecto es una aplicación ASP.NET Core que implementa un CRUD para gestionar productos y proveedores. Evoluciona desde un enfoque MVC tradicional hacia una API REST documentada con Swagger.

---

## Requisitos previos

Antes de comenzar, asegúrate de tener instaladas las siguientes herramientas:

1. **.NET SDK**: [Descargar aquí](https://dotnet.microsoft.com/download)
2. **SQL Server**: Para la base de datos.
3. **Visual Studio Code** o **Visual Studio**: Como entorno de desarrollo.
4. **Postman** (opcional): Para probar la API REST.

---

## Pasos para construir el proyecto

### 1. Crear el proyecto inicial

1. Abre una terminal y ejecuta el siguiente comando para crear un proyecto MVC:
   ```bash
   dotnet new mvc -n DemoMvc

2-Configurar base de datos
    1-Crea una base de datos en SQL Server llamada MVCProductosBD.
    2-Configura la conexión en Program.cs:

builder.Services.AddDbContext<DataContex>(option =>
{
    option.UseSqlServer("Server=DESKTOP-IAMK8JE\\SQLEXPRESS;Database=MVCProductosBD;Trusted_Connection=True;TrustServerCertificate=True");
});

3-Crea la clase DataContex en la carpeta Data para manejar el contexto de la base de datos:
public class DataContex : DbContext
{
    public DataContex(DbContextOptions<DataContex> options) : base(options) { }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
}
3.1-crear los modelos:

    1-Modelo Producto
        public class Producto
        {
            [Key]
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public decimal Precio { get; set; }
            public int CategoriaId { get; set; }
            public Categoria? Categoria { get; set; }
            public int ProveedorId { get; set; }
            public Proveedor? Proveedor { get; set; }
        }
    2-Modelo Proveedor
            public class Proveedor
        {
            [Key]
            public int Id { get; set; }
            public string Nombre { get; set; }
            public List<Producto> ListaProductos { get; set; }
        }
    


4. Crear el CRUD MVC
Controlador ProductoController:

Implementa las acciones para listar, crear, editar y eliminar productos.
Usa vistas Razor para la interfaz de usuario.
Controlador ProveedorController:

Similar al controlador de productos, pero para gestionar proveedores.
Vistas:

Crea vistas Razor para cada acción del controlador (Index, Create, Edit, Delete).

5. Evolución hacia una API REST
    1.Crear controladores para la API:

    ProductoApiController: Maneja las operaciones CRUD para productos.
    ProveedorApiController: Maneja las operaciones CRUD para proveedores.

    Ejemplo de un método en ProductoApiController:
        [HttpGet]
        public async Task<IActionResult> ObtenerProductos()
        {
            var productos = await _productoService.ObtenerTodosAsync();
            return Ok(productos);
        }

    2.Crear servicios:

    ProductoService: Contiene la lógica de negocio para productos.
    ProveedorService: Contiene la lógica de negocio para proveedores.
    Ejemplo de un método en ProductoService:

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Proveedor)
                .ToListAsync();
        }

    3.Registrar servicios en Program.cs:

    builder.Services.AddScoped<ProductoService>();
    builder.Services.AddScoped<ProveedorService>();


6. Integrar Swagger para documentar la API
    
    1.Instala el paquete NuGet de Swagger:
    dotnet add package Swashbuckle.AspNetCore
    
    2.Configura Swagger en Program.cs:
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API de Productos",
            Version = "v1",
            Description = "API REST para gestionar productos y proveedores."
        });
    });

    3.Habilita Swagger en el entorno de desarrollo:
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Productos v1");
            c.RoutePrefix = "swagger";
        });
    }

    4.Accede a Swagger en: http://localhost:<puerto>/swagger

    7. Solución a referencias cíclicas en JSON
Configura el serializador JSON para manejar referencias cíclicas en Program.cs:

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
    
8. Probar la API REST
Usa Swagger para probar los endpoints.
Alternativamente, usa Postman para enviar solicitudes HTTP a los endpoints de la API.



    

