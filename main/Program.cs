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
var evolveLogger = Log.ForContext("SourceContext", "Evolve");

var filterOptions = new HypermediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
builder.Services.AddSingleton(filterOptions);
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Gerenciamento com .NET",
        Version = "v1",
        Description = "Backend de um sistema de gerenciamento de recursos com c#."
    });
});
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImpl>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImpl>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepositoryImpl<>));
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
app.UseCors();
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