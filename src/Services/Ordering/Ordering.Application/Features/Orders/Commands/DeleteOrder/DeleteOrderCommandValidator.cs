using System;
using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
	public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
	{
		public DeleteOrderCommandValidator()
		{
			RuleFor(p => p.Id)
				.NotNull()
				.NotEmpty()
				.WithMessage("Id should not be null or empty.");
		}
	}
}

