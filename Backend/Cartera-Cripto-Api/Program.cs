using Cartera_Cripto.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//En esta parte registro la clase de base de datos (AppDBbContext) en
//el sistema de servicios de la aplicación, y le digo que use SQL Server
//como base de datos, usando la cadena de conexión llamada
//"Connection" que está en mi appsettings.json.


builder.Services.AddDbContext<AppDBcontext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

//Acá configuro CORS para que permita todas las peticiones

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Acá es donde aplico la política de CORS que definí anteriormente

app.UseCors("PermitirTodo");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
