using main.Services;
using main.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<IPersonService, PersonServiceImpl>();
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

