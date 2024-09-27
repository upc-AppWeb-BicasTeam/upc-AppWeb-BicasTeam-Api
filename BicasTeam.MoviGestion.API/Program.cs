using System.Text;
using BicasTeam.MoviGestion.API.Alerts.Application.Internal.CommandServices;
using BicasTeam.MoviGestion.API.Alerts.Application.Internal.QueryServices;
using BicasTeam.MoviGestion.API.Alerts.Domain.Repositories;
using BicasTeam.MoviGestion.API.Alerts.Domain.Services;
using BicasTeam.MoviGestion.API.Alerts.Infrastructure.Persistence.EFC.Repositories;
using BicasTeam.MoviGestion.API.IAM.Application.Internal.CommandServices;
using BicasTeam.MoviGestion.API.IAM.Application.Internal.OutboundServices;
using BicasTeam.MoviGestion.API.IAM.Application.Internal.QueryServices;
using BicasTeam.MoviGestion.API.IAM.Domain.Repositories;
using BicasTeam.MoviGestion.API.IAM.Domain.Services;
using BicasTeam.MoviGestion.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using BicasTeam.MoviGestion.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using BicasTeam.MoviGestion.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using BicasTeam.MoviGestion.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using BicasTeam.MoviGestion.API.IAM.Infrastructure.Tokens.JWT.Services;
using BicasTeam.MoviGestion.API.Profiles.Application.Internal.CommandServices;
using BicasTeam.MoviGestion.API.Profiles.Application.Internal.QueryServices;
using BicasTeam.MoviGestion.API.Profiles.Domain.Repositories;
using BicasTeam.MoviGestion.API.Profiles.Domain.Services;
using BicasTeam.MoviGestion.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            TermsOfService = new Uri("https://bicas-movigestion.com/tos"),
            Contact = new OpenApiContact{ Name = "Bicas Team MoviGestion", Email = "movigestion@bicas.com" },
            License = new OpenApiLicense { Name = "Apache 2.0", Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")},
        });
    });

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", policy => policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});



// Configurar JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenSettings.Issuer,
            ValidAudience = tokenSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),
            ClockSkew = TimeSpan.Zero  // Sin tolerancia de tiempo
        };
    });



// Agregar configuración para TokenSettings
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

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

// Profiles Bounded Context Injection Configuration
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();





var app = builder.Build();

// Verify Database Objects are Created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Production API");
        options.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add CORS Middleware with AllowAllPolicy
app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.Run();