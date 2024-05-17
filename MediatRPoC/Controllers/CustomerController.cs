using MediatR;
using MediatRPoC.Commands;
using MediatRPoC.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MediatRPoC.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly IMediator mediator;

		public CustomerController(IMediator mediator)
        {
			this.mediator = mediator;
		}

		[HttpPost("get-customers")]
		public async Task<ActionResult> GetCustomerByName([FromBody] GetCustomersQuery query)
		{
			try
			{
				var results = await mediator.Send(query);
				return Ok(results);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost("add-customer")]
		public async Task<ActionResult> AddCustomer([FromBody] CreateUserCommand command)
		{
			try
			{
				await mediator.Send(command);
				return Created();
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
