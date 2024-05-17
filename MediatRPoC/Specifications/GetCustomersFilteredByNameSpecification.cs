using MediatR;
using MediatRPoC.DTOs;
using MediatRPoC.Entities;

namespace MediatRPoC.Specifications
{
	internal class GetCustomersFilteredByNameSpecification : Specification<Customer, CustomerDto>
	{
		public GetCustomersFilteredByNameSpecification(string name)
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
