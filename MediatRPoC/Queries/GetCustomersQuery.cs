using System.Collections.Generic;
using MediatR;
using MediatRPoC.DTOs;
using MediatRPoC.Entities;
using MediatRPoC.Repositories;
using MediatRPoC.Specifications;

namespace MediatRPoC.Queries
{
	public class GetCustomersQuery : IRequest<List<CustomerDto>>
	{
		public string Name { get; }

		public GetCustomersQuery(string name)
		{
			Name = name;
		}
	}

	public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetCustomersQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<List<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
		{

			var customers = await _unitOfWork.Repository<Customer>().ListAsync(new CustomerForDtoSpecification(request.Name));
			return customers.ToList();
		}
	}
}
