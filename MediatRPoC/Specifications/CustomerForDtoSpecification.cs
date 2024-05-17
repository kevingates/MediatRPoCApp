using MediatR;
using MediatRPoC.DTOs;
using MediatRPoC.Entities;

namespace MediatRPoC.Specifications
{
	internal class CustomerForDtoSpecification : Specification<Customer, CustomerDto>
	{
		public CustomerForDtoSpecification(string name)
		{
			Where(c => c.Name.Contains(name))
				.OrderByExpression(q => q.OrderBy(x => x.Name))
				.SelectExpression(c => new CustomerDto
				{
					Id = c.Id,
					Name = c.Name
				});
		}
	}
}
