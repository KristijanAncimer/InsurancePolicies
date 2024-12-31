using InsurancePolicies.Application.Handlers.Policies.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicies.Api.Controllers;

public class PolicyController : Controller
{
    private readonly IMediator _mediator;
    public PolicyController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("Policy/AddPolicy")]
    public async Task<IActionResult> AddPolicy(string policyNumber, decimal policyAmount, [FromQuery]int partnerId)
    {
        try
        {
            var result = await _mediator.Send(CreatePolicyCommand.Create(policyNumber, policyAmount, partnerId));
            return Ok(result);
        }
        catch (Exception ex) 
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}