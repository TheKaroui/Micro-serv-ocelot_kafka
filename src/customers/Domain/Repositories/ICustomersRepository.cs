namespace customers.Domain.Repositories;
public interface ICustomersRepository{
    Task<IEnumerable<customers.Domain.Entities.Customers>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(customers.Domain.Entities.Customers e, CancellationToken ct = default);
}