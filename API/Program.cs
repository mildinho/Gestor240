using API.Biblioteca.JWT;
using Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

byte[] key = Encoding.ASCII.GetBytes(Settings.SecretKey);

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


//services cors
builder.Services.AddCors(p => p.AddPolicy("default", builder =>
{
    builder.WithOrigins("*").
    AllowAnyMethod().
    AllowAnyHeader();
}));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gestor240", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

builder.Services.AddInfraStructure(builder.Configuration);


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseCors("default");



app.UseAuthentication();
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
 * https://codewithmukesh.com/blog/dapper-in-aspnet-core/
 * https://macoratti.net/21/07/netcore_reptrandapp1.htm
 */