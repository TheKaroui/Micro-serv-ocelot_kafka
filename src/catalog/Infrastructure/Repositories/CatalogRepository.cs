using Microsoft.EntityFrameworkCore;
namespace catalog.Infrastructure.Repositories;
public class CatalogRepository : catalog.Domain.Repositories.ICatalogRepository {
    private readonly catalog.Infrastructure.AppDbContext _db; public CatalogRepository(catalog.Infrastructure.AppDbContext db){ _db=db; }
    public async Task<IEnumerable<catalog.Domain.Entities.Catalog>> GetAllAsync(CancellationToken ct=default) => await _db.Items.AsNoTracking().ToListAsync(ct);
    public async Task AddAsync(catalog.Domain.Entities.Catalog e, CancellationToken ct=default) => await _db.Items.AddAsync(e, ct);
}