using Prometheus;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// OpenTelemetry
builder.Services.AddOpenTelemetryTracing(tb => tb
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddJaegerExporter(o => { o.AgentHost = builder.Configuration["Jaeger:Host"] ?? "jaeger"; o.AgentPort = int.Parse(builder.Configuration["Jaeger:Port"] ?? "6831"); })
);
// Prometheus
builder.Services.AddSingleton<Prometheus.DotNetRuntime.DotNetRuntimeStatsCollector>();

// Register Kafka consumer background service
builder.Services.AddHostedService<catalog.Kafka.OrderEventsConsumer>();

var useInMemory = Environment.GetEnvironmentVariable("USE_INMEMORY") == "true" || builder.Configuration.GetValue<bool?>("UseInMemory") == true;

if(useInMemory) builder.Services.AddDbContext(catalog.Infrastructure.AppDbContext>(o => o.UseInMemoryDatabase("catalog-dev"));
else builder.Services.AddDbContext(catalog.Infrastructure.AppDbContext>(o => o.UseInMemoryDatabase("catalog-dev")); // replace with SQL in prod

builder.Services.AddScoped(catalog.Infrastructure.Repositories.CatalogRepository>();
builder.Services.AddScoped(catalog.Domain.Repositories.ICatalogRepository, catalog.Infrastructure.Repositories.CatalogRepository>();

builder.Services.AddAutoMapper(typeof(catalog.Application.Mapping.MappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(catalog.Application.Validators.CreateCatalogValidator>();

// JWT (dev)
var jwtKey = builder.Configuration["Jwt:Key"] ?? "dev-key-should-be-32chars-minimum!!!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1", new OpenApiInfo{{ Title = "Catalog API", Version = "v1" }});
});

var app = builder.Build();
app.UseMiddleware<Prometheus.DotNetRuntime.DotNetRuntimeStatsCollector>();
if(app.Environment.IsDevelopment()){{ app.UseSwagger(); app.UseSwaggerUI(); }}
app.UseAuthentication(); app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new {{ service = "catalog", status = "ok" }}));

// Simple collection endpoints
app.MapGet($"/api/catalog", async (catalog.Domain.Repositories.ICatalogRepository repo) => Results.Ok(await repo.GetAllAsync()));
app.MapPost($"/api/catalog", async (catalog.Domain.Models.CreateCatalogDto dto, catalog.Domain.Repositories.ICatalogRepository repo, catalog.Infrastructure.AppDbContext db) => {{
    var entity = dto.ToEntity();
    await repo.AddAsync(entity);
    await db.SaveChangesAsync();
    // Produce event to Kafka (placeholder)
    return Results.Created($"/api/catalog/{{entity.Id}}", entity);
}});

app.MapMetrics();
app.Run();
public partial class Program {{ }}