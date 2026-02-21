namespace orders.Domain.Models;
public record OrdersDto(Guid Id, string Name);
public record CreateOrdersDto(string Name){ public orders.Domain.Entities.Orders ToEntity() => new orders.Domain.Entities.Orders(Name); }