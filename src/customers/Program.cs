using Prometheus;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var useInMemory = Environment.GetEnvironmentVariable("USE_INMEMORY") == "true" || builder.Configuration.GetValue<bool?>("UseInMemory") == true;

if(useInMemory) builder.Services.AddDbContext(customers.Infrastructure.AppDbContext>(o => o.UseInMemoryDatabase("customers-dev"));
else builder.Services.AddDbContext(customers.Infrastructure.AppDbContext>(o => o.UseInMemoryDatabase("customers-dev")); // replace with SQL in prod

builder.Services.AddScoped(customers.Infrastructure.Repositories.CustomersRepository>();
builder.Services.AddScoped(customers.Domain.Repositories.ICustomersRepository, customers.Infrastructure.Repositories.CustomersRepository>();

builder.Services.AddAutoMapper(typeof(customers.Application.Mapping.MappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(customers.Application.Validators.CreateCustomersValidator>();

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
    c.SwaggerDoc("v1", new OpenApiInfo{{ Title = "Customers API", Version = "v1" }});
});

var app = builder.Build();
app.UseMiddleware<Prometheus.DotNetRuntime.DotNetRuntimeStatsCollector>();
if(app.Environment.IsDevelopment()){{ app.UseSwagger(); app.UseSwaggerUI(); }}
app.UseAuthentication(); app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new {{ service = "customers", status = "ok" }}));

// Simple collection endpoints
app.MapGet($"/api/customers", async (customers.Domain.Repositories.ICustomersRepository repo) => Results.Ok(await repo.GetAllAsync()));
app.MapPost($"/api/customers", async (customers.Domain.Models.CreateCustomersDto dto, customers.Domain.Repositories.ICustomersRepository repo, customers.Infrastructure.AppDbContext db) => {{
    var entity = dto.ToEntity();
    await repo.AddAsync(entity);
    await db.SaveChangesAsync();
    // Produce event to Kafka (placeholder)
    return Results.Created($"/api/customers/{{entity.Id}}", entity);
}});

app.MapMetrics();
app.Run();
public partial class Program {{ }}