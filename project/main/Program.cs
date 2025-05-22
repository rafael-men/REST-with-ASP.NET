using System;
using main.Data;
using main.Business;
using main.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using main.Repository;
using main.Repository.Implementations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Evolve;
using Npgsql;
using Serilog;
using Microsoft.Extensions.Configuration;
using main.Hypermedia.Filters;
using main.Hypermedia.Enricher;
using main.Security;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
var connection = builder.Configuration.GetConnectionString("PostgresqlConnection");
builder.Services.AddDbContext<PostgreSqlContext>(options =>
    options.UseNpgsql(connection)
);


builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);


var filterOptions = new HypermediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
builder.Services.AddSingleton(filterOptions);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var key = Encoding.ASCII.GetBytes("MySuperSecureKeyThatHasAtLeast32Chars");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = "Mine",
        ValidateAudience = true,
        ValidAudience = "Mine",
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
              .RequireAuthenticatedUser();
    });
});




builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Gerenciamento com .NET",
        Version = "v1",
        Description = "Backend de um sistema de gerenciamento de recursos com C#."
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT (com o prefixo 'Bearer').",
        BearerFormat = "JWT",
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImpl>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryImpl<>));
builder.Services.AddScoped<ILoginBusiness, LoginBusinessImpl>();
builder.Services.AddScoped<ITokenService, TokenServiceImpl>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    var logger = scope.ServiceProvider.GetRequiredService<Serilog.ILogger>();
    DatabaseMigration.MigrateDatabase(configuration, logger.ForContext("SourceContext", "Evolve"));
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Gerenciamento");
    });
}

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute("", "{controller=values}/{id?}");
app.Run();


public static class DatabaseMigration
{
    public static void MigrateDatabase(IConfiguration configuration, Serilog.ILogger logger)
    {
        try
        {
            logger.Information("Iniciando as migrações de banco de dados com Evolve.");
            var connectionString = configuration.GetConnectionString("PostgresqlConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                logger.Error("A string de conexão 'PostgresqlConnection' não foi configurada.");
                return;
            }

            using (var connection = new NpgsqlConnection(connectionString))
            {
                var evolve = new Evolve.Evolve(connection, message => logger.Information(message))
                {
                    Locations = new[] { "db/migrations" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();

                logger.Information("Migrações de banco de dados concluídas com sucesso.");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Ocorreu um erro durante as migrações de banco de dados.");
            throw;
        }
    }
}
