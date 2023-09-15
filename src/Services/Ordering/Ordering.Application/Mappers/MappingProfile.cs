using System;
using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckOutOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
            CreateMap<Order, OrderVm>().ReverseMap();
            CreateMap<Order, CheckOutOrderCommand>().ReverseMap();
        }
	}
}

