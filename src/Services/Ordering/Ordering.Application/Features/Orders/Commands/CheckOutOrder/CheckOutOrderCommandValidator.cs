using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
	public class CheckOutOrderCommandValidator : AbstractValidator<CheckOutOrderCommand>
	{
		public CheckOutOrderCommandValidator()
		{
			RuleFor(p => p.UserName)
				.NotEmpty().WithMessage("must provide the {UserName}")
				.NotNull()
				.MaximumLength(50).WithMessage("{UserName} must not be exceed 50 characters");

			RuleFor(p => p.EmailAddress)
				.NotEmpty().WithMessage("must provide {EmailAddress}");

			RuleFor(p => p.TotalPrice)
				.NotEmpty().WithMessage("must provide {TotalPrice}")
				.GreaterThan(0).WithMessage("{TotalPrice} must be greater than 0");
		}
	}
}

