using Prometheus;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional:false, reloadOnChange:true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo{ Title = "Ocelot Gateway", Version = "v1" }); });
var app = builder.Build();
app.UseMiddleware<Prometheus.DotNetRuntime.DotNetRuntimeStatsCollector>();
if(app.Environment.IsDevelopment()){ app.UseSwagger(); app.UseSwaggerUI(); }
app.MapGet("/", () => Results.Ok(new { status = "ocelot-gateway" }));
await app.UseOcelot();
app.MapMetrics();
app.Run();
public partial class Program { }