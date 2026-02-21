using Prometheus;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// OpenTelemetry (Tracing + Metrics) and Prometheus
builder.Services.AddOpenTelemetryTracing(tb => tb
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .AddJaegerExporter(o => { o.AgentHost = builder.Configuration["Jaeger:Host"] ?? "jaeger"; o.AgentPort = int.Parse(builder.Configuration["Jaeger:Port"] ?? "6831"); })
);
builder.Services.AddControllers();
// Prometheus metrics
builder.Services.AddSingleton<Prometheus.DotNetRuntime.DotNetRuntimeStatsCollector>();

var useInMemory = Environment.GetEnvironmentVariable("USE_INMEMORY") == "true" || builder.Configuration.GetValue<bool?>("UseInMemory") == true;

if(useInMemory) builder.Services.AddDbContext(orders.Infrastructure.AppDbContext>(o => o.UseInMemoryDatabase("orders-dev"));
else builder.Services.AddDbContext(orders.Infrastructure.AppDbContext>(o => o.UseInMemoryDatabase("orders-dev")); // replace with SQL in prod

builder.Services.AddScoped(orders.Infrastructure.Repositories.OrdersRepository>();
builder.Services.AddScoped(orders.Domain.Repositories.IOrdersRepository, orders.Infrastructure.Repositories.OrdersRepository>();

builder.Services.AddAutoMapper(typeof(orders.Application.Mapping.MappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(orders.Application.Validators.CreateOrdersValidator>();

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
    c.SwaggerDoc("v1", new OpenApiInfo{{ Title = "Orders API", Version = "v1" }});
});

var app = builder.Build();
app.UseMiddleware<Prometheus.DotNetRuntime.DotNetRuntimeStatsCollector>();
if(app.Environment.IsDevelopment()){{ app.UseSwagger(); app.UseSwaggerUI(); }}
app.UseAuthentication(); app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new {{ service = "orders", status = "ok" }}));

// Simple collection endpoints
app.MapGet($"/api/orders", async (orders.Domain.Repositories.IOrdersRepository repo) => Results.Ok(await repo.GetAllAsync()));
app.MapPost($"/api/orders", async (orders.Domain.Models.CreateOrdersDto dto, orders.Domain.Repositories.IOrdersRepository repo, orders.Infrastructure.AppDbContext db) => {{
    var entity = dto.ToEntity();
    await repo.AddAsync(entity);
    await db.SaveChangesAsync();
    // Produce event to Kafka (placeholder)
    return Results.Created($"/api/orders/{{entity.Id}}", entity);
}});

app.MapMetrics();
app.Run();
public partial class Program {{ }}