using Microsoft.EntityFrameworkCore;
namespace orders.Infrastructure.Repositories;
public class OrdersRepository : orders.Domain.Repositories.IOrdersRepository {
    private readonly orders.Infrastructure.AppDbContext _db; public OrdersRepository(orders.Infrastructure.AppDbContext db){ _db=db; }
    public async Task<IEnumerable<orders.Domain.Entities.Orders>> GetAllAsync(CancellationToken ct=default) => await _db.Items.AsNoTracking().ToListAsync(ct);
    public async Task AddAsync(orders.Domain.Entities.Orders e, CancellationToken ct=default) => await _db.Items.AddAsync(e, ct);
}