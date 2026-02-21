namespace catalog.Domain.Models;
public record CatalogDto(Guid Id, string Name);
public record CreateCatalogDto(string Name){ public catalog.Domain.Entities.Catalog ToEntity() => new catalog.Domain.Entities.Catalog(Name); }