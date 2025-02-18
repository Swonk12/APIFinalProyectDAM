using APIFinalProyectDAM.DATA;
using Microsoft.EntityFrameworkCore;  // Importa el espacio de nombres de tu DbContext


var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión a SQL Server (ya debe estar en appsettings.json)
builder.Services.AddDbContext<ClDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Asegúrate de que "DefaultConnection" esté configurado correctamente en appsettings.json



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
