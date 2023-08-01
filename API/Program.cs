using System.Configuration;
using API;
using Infra.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfraStructure(builder.Configuration);


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



/*
 * TODO: NO CONTROLADOR DA EMPRESA NA OPERACAO CRUD TRAVAR A UNIDADE FEDERATIVA
 * TODO: TRANSFORMAR O ENUM TIPOINSCRICAOEMPRESA EM TABELA;
 * TODO: FAZER O CRUD DA TABELA AGENCIA;
 * TODO: AJUSTAR OS CAMPOS CONFORME O LAYOUT - TABELA HEADERARQUIVO;
 * TODO: AJUSTAR OS CAMPOS CONFORME O LAYOUT - TABELA HEADERLOTE;
 * TODO: AJUSTAR OS CAMPOS CONFORME O LAYOUT - TABELA TRAILERARQUIVO;
 * TODO: AJUSTAR OS CAMPOS CONFORME O LAYOUT - TABELA TRAILERLOTE;
 */