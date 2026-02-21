using AutoMapper;
namespace catalog.Application.Mapping;
public class MappingProfile : Profile{ public MappingProfile(){ CreateMap<catalog.Domain.Entities.Catalog, catalog.Domain.Models.CatalogDto>(); } }