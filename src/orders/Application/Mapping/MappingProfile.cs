using AutoMapper;
namespace orders.Application.Mapping;
public class MappingProfile : Profile{ public MappingProfile(){ CreateMap<orders.Domain.Entities.Orders, orders.Domain.Models.OrdersDto>(); } }