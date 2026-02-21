namespace orders.Domain.Repositories;
public interface IOrdersRepository{
    Task<IEnumerable<orders.Domain.Entities.Orders>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(orders.Domain.Entities.Orders e, CancellationToken ct = default);
}