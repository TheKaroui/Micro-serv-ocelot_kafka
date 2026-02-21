using Microsoft.EntityFrameworkCore;
namespace customers.Infrastructure.Repositories;
public class CustomersRepository : customers.Domain.Repositories.ICustomersRepository {
    private readonly customers.Infrastructure.AppDbContext _db; public CustomersRepository(customers.Infrastructure.AppDbContext db){ _db=db; }
    public async Task<IEnumerable<customers.Domain.Entities.Customers>> GetAllAsync(CancellationToken ct=default) => await _db.Items.AsNoTracking().ToListAsync(ct);
    public async Task AddAsync(customers.Domain.Entities.Customers e, CancellationToken ct=default) => await _db.Items.AddAsync(e, ct);
}