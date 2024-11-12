
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VOLXYSEAT.API.Application.Commands.MercadoPago.CreatePreApproval;
using VOLXYSEAT.API.Application.Commands.MercadoPago.CreatePreApprovalPlan;


namespace VOLXYSEAT.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class Payment(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("create-preapproval-plan")]
    public async Task<IActionResult> CreatePreApprovalPlan(CreatePreApprovalPlanCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("create-preapproval")]
    public async Task<IActionResult> CreatePreApproval([FromBody] CreatePreApprovalCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }
}

