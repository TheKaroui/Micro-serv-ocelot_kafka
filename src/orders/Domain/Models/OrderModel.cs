namespace orders.Domain.Models;
public record OrderDto(System.Guid Id, string Name);
public record CreateOrderDto(string Name) { public orders.Domain.Entities.Orders ToEntity() => new orders.Domain.Entities.Orders(Name); }
