﻿using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderVm>>
	{
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(
            IOrderRepository orderRepository,
            IMapper mapper
            )
		{
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        async Task<List<OrderVm>> IRequestHandler<GetOrdersListQuery, List<OrderVm>>.Handle
            (
                GetOrdersListQuery request,
                CancellationToken cancellationToken
            )
        {
            var orderList = (await _orderRepository.GetOrdersByUserName(request.UserName));

            return _mapper.Map<List<OrderVm>>(orderList);
        }
    }
}
