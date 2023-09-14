using System;
using Ordering.Application.Models;

namespace Ordering.Application.Contracts.Infrastructure
{
	public interface IEmailServices
	{
		Task<bool> SendEmail(Email email);
	}
}

