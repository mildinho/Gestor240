using System.Configuration;
using System.Text;
using API;
using API.Biblioteca.JWT;
using Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

byte[] key = Encoding.ASCII.GetBytes(Settings.Chave);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
//AddJwtBearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

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

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



/*
 * TODO: FAZER O CRUD DA TABELA AGENCIA;
 * TODO: AJUSTAR OS CAMPOS CONFORME O LAYOUT - TABELA HEADERARQUIVO;
 * TODO: AJUSTAR OS CAMPOS CONFORME O LAYOUT - TABELA HEADERLOTE;
 * TODO: AJUSTAR OS CAMPOS CONFORME O LAYOUT - TABELA TRAILERARQUIVO;
 * TODO: AJUSTAR OS CAMPOS CONFORME O LAYOUT - TABELA TRAILERLOTE;
 * TODO: ENTENDER QUAL A DIFERENCA DO G100 x G103
 * 
 * --- fontes/remessa.prg --> layout
 * https://themewagon.com/themes/free-bootstrap-4-html-5-admin-dashboard-website-template-ruang/
 * https://themewagon.github.io/ruang-admin/datatables.html
 * https://www.codeproject.com/Articles/741207/Repository-with-Unit-of-Work-IoC-and-Unit-Test
 */