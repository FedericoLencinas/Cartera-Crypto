using Cartera_Cripto.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//En esta parte registro la clase de base de datos (AppDBbContext) en
//el sistema de servicios de la aplicaci�n, y le digo que use SQL Server
//como base de datos, usando la cadena de conexi�n llamada
//"Connection" que est� en mi appsettings.json.


builder.Services.AddDbContext<AppDBcontext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

//Ac� configuro CORS para que permita todas las peticiones

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

//Ac� es donde aplico la pol�tica de CORS que defin� anteriormente

app.UseCors("PermitirTodo");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
