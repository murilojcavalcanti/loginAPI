using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using usuarioApi.Authorization;
using usuarioApi.Data;
using usuarioApi.Models;
using usuarioApi.Services;

var builder = WebApplication.CreateBuilder(args);

//add connection database
//uso mais simples
//var connectionString = builder.Configuration.GetConnectionString("ApiConnection");
//usando secrets
var connectionString = builder.Configuration["ConnectionStrings:ApiConnection"];

builder.Services.AddDbContext<UsuarioDbContext>
    (opts =>
    {
        opts.UseMySql(connectionString,
                  ServerVersion.AutoDetect(connectionString));
    });

 //add Identity
builder.Services
    .AddIdentity<Usuario,IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

//add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//injetando a dependencia IAuthorizationHandler do tipo IdadeAuthorization
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adicionando as configurações do jwt
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Valida a chave do token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration
        ["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

//adiciona as regras de autorização do usuario, neste caso uma idade minima para o login!
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima",policy=>
            policy.AddRequirements(new IdadeMinima(18)));
});

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//mostra o uso de autemticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
