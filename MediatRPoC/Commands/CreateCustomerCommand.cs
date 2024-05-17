using MediatR;
using MediatRPoC.Entities;
using MediatRPoC.Repositories;

namespace MediatRPoC.Commands;
public class CreateUserCommand : IRequest
{
	public string Name { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{

	private readonly IUnitOfWork _unitOfWork;

	public CreateUserCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var customer = new Customer { Name = request.Name };
		await _unitOfWork.Repository<Customer>().AddAsync(customer);
		await _unitOfWork.CompleteAsync();
	}
}
