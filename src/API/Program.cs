using System.IO;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Application.Services;
using Core.Interfaces;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- Carrega JSON, UserSecrets e EnvironmentVars automaticamente ---
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>(optional: true);

// Monta o caminho absoluto para o arquivo SQLite
var contentRoot = builder.Environment.ContentRootPath;
var sqliteFile   = builder.Configuration.GetConnectionString("DefaultConnection")
                     ?? throw new InvalidOperationException("ConnectionString 'DefaultConnection' não encontrada.");
var dbFullPath   = Path.Combine(contentRoot, sqliteFile);

// 1. Configurar EF Core (SQLite)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite($"Data Source={dbFullPath}"));

// 2. Injeção de dependência das camadas
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();

// 3. Controllers
builder.Services.AddControllers();

// 4. Configurar JWT
var jwtKey = builder.Configuration["Jwt:Key"]
             ?? throw new InvalidOperationException("JWT key is not configured.");
var key    = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata      = false;
    options.SaveToken                 = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey      = true,
        IssuerSigningKey              = new SymmetricSecurityKey(key),
        ValidateIssuer                = false,
        ValidateAudience              = false,
        ValidateLifetime              = true
    };
});

// 5. Swagger com suporte a Bearer
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchDotNet API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Ex: \"Bearer {token}\"",
        Name        = "Authorization",
        In          = ParameterLocation.Header,
        Type        = SecuritySchemeType.ApiKey,
        Scheme      = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// 6. Aplicar migrations e seed inicial
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();

        if (!db.Users.Any())
        {
            db.Users.Add(new Core.Entities.User("admin", "admin123"));
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Falha ao migrar/seed no banco.");
        throw; // para não subir a API em estado inconsistente
    }
}

// 7. Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchDotNet v1"));
}

app.UseHttpsRedirection();
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
