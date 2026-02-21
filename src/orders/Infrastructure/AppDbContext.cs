using Microsoft.EntityFrameworkCore;
namespace orders.Infrastructure;
public class AppDbContext : DbContext{ public AppDbContext(DbContextOptions<AppDbContext> opts): base(opts){} public DbSet<orders.Domain.Entities.Orders> Items => Set<orders.Domain.Entities.Orders>(); }