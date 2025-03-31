using System;
using main.Data;
using main.Business;
using main.Business.Implementations;
using Microsoft.EntityFrameworkCore;
using main.Repository;
using main.Repository.Implementations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PostgreSqlContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlConnection"))
);
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
builder.Services.AddScoped<IPersonRepository,PersonRepositoryImpl>();
var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Gerenciamento");
    });
}

app.MapControllers();
app.Run();

