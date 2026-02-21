using AutoMapper;
namespace customers.Application.Mapping;
public class MappingProfile : Profile{ public MappingProfile(){ CreateMap<customers.Domain.Entities.Customers, customers.Domain.Models.CustomersDto>(); } }