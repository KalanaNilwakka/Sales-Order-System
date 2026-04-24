using AutoMapper;
using Sales_Order_System_Backend.API.DTOs;
using Sales_Order_System_Backend.Domain.Entities;

namespace Sales_Order_System_Backend.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientReadDTO>();

        CreateMap<Item, ItemReadDTO>();

        CreateMap<Order, OrderReadDTO>();
        CreateMap<OrderCreateDTO, Order>();

        CreateMap<OrderItem, OrderItemReadDTO>();
        CreateMap<OrderItemCreateDTO, OrderItem>();
    }
}