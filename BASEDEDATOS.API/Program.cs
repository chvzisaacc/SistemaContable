using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURACIÓN DE PORTABILIDAD ---
// Esto le dice al servidor que escuche en el puerto 5145 de cualquier IP de la PC (0.0.0.0)
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5145);
});
// -------------------------------------

// 1. Configurar límites de Kestrel y FormOptions para manejar hashes largos
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

// 2. Configurar JSON para que ignore mayúsculas/minúsculas (evita errores de mapeo)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapGet("/", () => Results.Ok(new
{
    mensaje = "Aún no se han enviado datos",
    estado = "en espera",
    servidor = "BASEDEDATOS.API activo",
    fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
}));


app.MapControllers();

app.Run("http://0.0.0.0:5145");