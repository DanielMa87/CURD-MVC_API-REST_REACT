using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Importa el espacio de nombres para Swagger
using MVC_Productos.Data;
using MVC_Productos.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios para la aplicación

// Configuración para controladores con vistas (MVC)
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        // Configuración para manejar referencias cíclicas en JSON (útil para relaciones complejas en EF Core)
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        // Formatea el JSON con sangría para facilitar la lectura (solo para vistas MVC)
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Configuración para controladores de API REST
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ignora referencias cíclicas para evitar errores al serializar objetos relacionados
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        // Excluye propiedades con valores nulos en las respuestas JSON
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Configuración de Swagger para documentar la API REST
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Productos", // Título de la documentación
        Version = "v1", // Versión de la API
        Description = "API REST para gestionar productos y proveedores." // Descripción de la API
    });
});

// Configuración de la base de datos con Entity Framework Core
builder.Services.AddDbContext<DataContex>(options =>
{
    // Conexión a la base de datos SQL Server
    options.UseSqlServer("Server=DESKTOP-IAMK8JE\\SQLEXPRESS;Database=MVCProductosBD;Trusted_Connection=True;TrustServerCertificate=True");
});

// Registro del servicio de productos para inyección de dependencias
builder.Services.AddScoped<ProductoService>();

// Configuración de CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirReact", builder =>
    {
        // Permite solicitudes desde la aplicación React/vite en localhost:5173
        builder.WithOrigins("http://localhost:5173") // Cambia al puerto de React si es necesario
               .AllowAnyHeader() // Permite cualquier encabezado en las solicitudes
               .AllowAnyMethod(); // Permite cualquier método HTTP (GET, POST, PUT, DELETE, etc.)
    });
});

var app = builder.Build();

// Configuración del pipeline de manejo de solicitudes HTTP

// Aplica la política de CORS configurada anteriormente
app.UseCors("PermitirReact");

// Configuración para entornos que no son de desarrollo (producción, pruebas, etc.)
if (!app.Environment.IsDevelopment())
{
    // Muestra una página de error genérica en caso de excepciones
    app.UseExceptionHandler("/Home/Error");
    // Habilita HSTS (HTTP Strict Transport Security) para mejorar la seguridad en producción
    app.UseHsts();
}

// Habilitar Swagger solo en el entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Genera la documentación Swagger
    app.UseSwaggerUI(c =>
    {
        // Configura la ruta para acceder a la documentación Swagger
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Productos v1");
        c.RoutePrefix = "swagger"; // Ruta base para Swagger (http://localhost:<puerto>/swagger)
    });
}

// Redirige automáticamente las solicitudes HTTP a HTTPS
app.UseHttpsRedirection();

// Habilita el enrutamiento para manejar las solicitudes
app.UseRouting();

// Habilita la autorización (aunque no se está configurando autenticación en este proyecto)
app.UseAuthorization();

// Habilita el uso de archivos estáticos (como CSS, JS, imágenes, etc.)
app.UseStaticFiles();

// Configura el enrutamiento para los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Ruta predeterminada: controlador "Home", acción "Index"

// Inicia la aplicación
app.Run();
