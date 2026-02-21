namespace customers.Domain.Models;
public record CustomersDto(Guid Id, string Name);
public record CreateCustomersDto(string Name){ public customers.Domain.Entities.Customers ToEntity() => new customers.Domain.Entities.Customers(Name); }