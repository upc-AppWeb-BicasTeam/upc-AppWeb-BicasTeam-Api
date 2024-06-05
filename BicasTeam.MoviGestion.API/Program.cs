using BicasTeam.MoviGestion.API.Alerts.Application.Internal.CommandServices;
using BicasTeam.MoviGestion.API.Alerts.Application.Internal.QueryServices;
using BicasTeam.MoviGestion.API.Alerts.Domain.Repositories;
using BicasTeam.MoviGestion.API.Alerts.Domain.Services;
using BicasTeam.MoviGestion.API.Alerts.Infrastructure.Persistence.EFC.Repositories;

using BicasTeam.MoviGestion.API.Shared.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BicasTeam.MoviGestion.API.Shared.Infrastructure.Persistence.EFC.Repositories;

using BicasTeam.MoviGestion.API.Vehicles.Application.Internal.CommandServices;
using BicasTeam.MoviGestion.API.Vehicles.Application.Internal.QueryServices;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Vehicles.Domain.Services;
using BicasTeam.MoviGestion.API.Vehicles.Infrastructure.Persistence.EFC.Repositories;

using BicasTeam.MoviGestion.API.Shipments.Application.Internal.CommandServices;
using BicasTeam.MoviGestion.API.Shipments.Application.Internal.QueryServices;
using BicasTeam.MoviGestion.API.Shipments.Domain.Repositories;
using BicasTeam.MoviGestion.API.Shipments.Domain.Services;
using BicasTeam.MoviGestion.API.Shipments.Infrastructure.Persistence.EFC.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Configuration for Routing

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (connectionString == null) return;
    if (builder.Environment.IsDevelopment()) 
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction()) 
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title   = "BicasTeam.MoviGestion.API",
            Version = "v1",
            Description = "BicasTeam MoviGestion Platform API",
            TermsOfService = new Uri("https://acme-learning.com/tos"),
            Contact = new OpenApiContact{ Name = "Bicas Team MoviGestion", Email = "movigestion@bicas.com" },
            License = new OpenApiLicense { Name = "Apache 2.0", Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")},
        });
    });

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Vehicles Bounded Context Injection Configuration
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleCommandService, VehicleCommandService>();
builder.Services.AddScoped<IVehicleQueryService, VehicleQueryService>();

// Reports Bounded Context Injection Configuration
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();

// Shipments Bounded Context Injection Configuration
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IShipmentCommandService, ShipmentCommandService>();
builder.Services.AddScoped<IShipmentQueryService, ShipmentQueryService>();


var app = builder.Build();

// Verify Database Objects are Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();