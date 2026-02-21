namespace catalog.Domain.Entities;
public sealed class Catalog{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    private Catalog(){}
    public Catalog(string name){ if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name"); Id = Guid.NewGuid(); Name = name; }
}