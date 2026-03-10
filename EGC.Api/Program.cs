using Application;
using EGC.Api;
using EGC.Api.Extensions;
using Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

//Configuracion de DateOnly


//CORS Config
var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsOptions =>
    {
        corsOptions.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(origenesPermitidos)
                    .WithExposedHeaders("cantidadTotalRegistros");
    });
});

WebApplication app = builder.Build();

RouteGroupBuilder apiGroup = app.MapGroup("/api/v1");
app.MapEndpoints(apiGroup);

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();

    app.ApplyMigrations();
}

app.UseExceptionHandler();

//app.UseAuthentication();

//app.UseAuthorization();

await app.RunAsync();
