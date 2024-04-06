using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using nuget_fiap_app_produto.Services;
using nuget_fiap_app_produto_common.Interfaces.Repository;
using nuget_fiap_app_produto_common.Interfaces.Services;
using nuget_fiap_app_produto_repository;
using nuget_fiap_app_produto_repository.DB;
using nuget_fiap_app_produto_repository.Interface;

var builder = WebApplication.CreateBuilder(args);


//Services
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

//Repositórios
builder.Services.AddScoped<RepositoryDB>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();


//HealthCheck
builder.Services.AddHealthChecks();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NuGET Burger",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Miro",
            Url = new Uri("https://miro.com/app/board/uXjVMqYSzbg=/?share_link_id=124875092732")

        }
    });

    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseReDoc(c =>
{
    c.DocumentTitle = "REDOC API Documentation";
    c.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    },
});

app.Run();
