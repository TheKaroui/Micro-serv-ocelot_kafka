namespace catalog.Domain.Repositories;
public interface ICatalogRepository{
    Task<IEnumerable<catalog.Domain.Entities.Catalog>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(catalog.Domain.Entities.Catalog e, CancellationToken ct = default);
}