using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

/// <summary>
/// Configura el servidor Kestrel para escuchar en el puerto 5145 de cualquier interfaz de red (0.0.0.0).
/// </summary>
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5145);
});
// -------------------------------------

/// <summary>
/// Configura los límites de Kestrel y FormOptions para manejar valores y hashes largos sin restricciones.
/// </summary>
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

/// <summary>
/// Configura los controladores y las opciones de serialización JSON para ignorar mayúsculas/minúsculas
/// y evitar errores en el mapeo de propiedades.
/// </summary>
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/// <summary>
/// Configura el pipeline de solicitudes HTTP. En modo desarrollo, habilita Swagger y la interfaz de usuario Swagger.
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

/// <summary>
/// Mapea una ruta GET raíz que retorna un mensaje de estado del servidor en formato JSON.
/// </summary>
app.MapGet("/", () => Results.Ok(new
{
    mensaje = "Aún no se han enviado datos",
    estado = "en espera",
    servidor = "BASEDEDATOS.API activo",
    fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
}));


app.MapControllers();

app.Run("http://0.0.0.0:5145");