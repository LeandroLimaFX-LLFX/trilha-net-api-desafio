using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conexao = builder.Configuration.GetConnectionString("ConexaoPadrao");
if (!string.IsNullOrEmpty(conexao) && !conexao.Contains("COLOCAR"))
{
    builder.Services.AddDbContext<OrganizadorContext>(options =>
        options.UseSqlServer(conexao));
}
else
{
    // Quando não houver connection string configurada (ex: exercício local sem SQL Server),
    // usamos um provedor InMemory para permitir execução e testes locais.
    builder.Services.AddDbContext<OrganizadorContext>(options =>
        options.UseInMemoryDatabase("TrilhaApiDesafio"));
}

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
