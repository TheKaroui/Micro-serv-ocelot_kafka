namespace orders.Domain.Entities;
public sealed class Orders{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    private Orders(){}
    public Orders(string name){ if(string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name"); Id = Guid.NewGuid(); Name = name; }
}