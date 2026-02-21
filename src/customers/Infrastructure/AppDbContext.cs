using Microsoft.EntityFrameworkCore;
namespace customers.Infrastructure;
public class AppDbContext : DbContext{ public AppDbContext(DbContextOptions<AppDbContext> opts): base(opts){} public DbSet<customers.Domain.Entities.Customers> Items => Set<customers.Domain.Entities.Customers>(); }