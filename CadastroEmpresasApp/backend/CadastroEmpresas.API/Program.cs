using CadastroEmpresas.API.Models;
using CadastroEmpresas.API.Services;
using CadastroEmpresas.Domain.Interfaces;
using CadastroEmpresas.Infrastructure.Data;
using CadastroEmpresas.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Conexão com o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Injeção de dependências
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<TokenService>();

// Configurações de JWT (appsettings.json)
builder.Services.Configure<ConfiguracoesToken>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<ConfiguracoesToken>();

if (string.IsNullOrEmpty(jwtSettings?.ChaveSecreta))
    throw new Exception("ChaveSecreta do JWT não foi configurada corretamente.");

var key = Encoding.UTF8.GetBytes(jwtSettings.ChaveSecreta);

// Configuração da autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; 
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Emissor,
        ValidAudience = jwtSettings.Audiencia,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// CORS para permitir o frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:8080") // URL do frontend
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
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

app.UseCors("PermitirFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
