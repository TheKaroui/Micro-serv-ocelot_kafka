namespace customers.Domain.Entities;
public sealed class Customers{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    private Customers(){}
    public Customers(string name){ if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name"); Id = Guid.NewGuid(); Name = name; }
}