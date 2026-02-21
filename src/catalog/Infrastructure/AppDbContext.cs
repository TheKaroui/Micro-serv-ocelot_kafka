using Microsoft.EntityFrameworkCore;
namespace catalog.Infrastructure;
public class AppDbContext : DbContext{ public AppDbContext(DbContextOptions<AppDbContext> opts): base(opts){} public DbSet<catalog.Domain.Entities.Catalog> Items => Set<catalog.Domain.Entities.Catalog>(); }