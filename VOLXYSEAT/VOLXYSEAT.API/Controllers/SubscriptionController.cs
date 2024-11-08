using MediatR;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using VOLXYSEAT.API.Application.Request;
using Volxyseat.API.Application.Queries.Subscription;
using VOLXYSEAT.API.Application.Commands.Subscription.Close;
using VOLXYSEAT.API.Application.Commands.Subscription.Create;
using VOLXYSEAT.API.Application.Models.ViewModel.Subscription;
using Microsoft.AspNetCore.Authorization;

namespace VOLXYSEAT.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubscriptionController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(IMediator mediator, ILogger<SubscriptionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SubscriptionViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SubscriptionViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetSubscriptionQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }


        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SubscriptionViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllSubscriptionQuery();
            var subscriptions = await _mediator.Send(query);

            if (subscriptions == null || !subscriptions.Any())
                return NotFound("No subscriptions found.");

            return Ok(subscriptions);
        }

        [HttpPost("/new-subscription")]
        [ProducesResponseType(typeof(SubscriptionViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateSubscriptionCommand request)
        {
            var result = await _mediator.Send(request);
            return !result ? Ok() : BadRequest();
        }

        [HttpPost("{id:Guid}/states/action=close")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Close(Guid id, [FromBody] SubscriptionChangeStatusWithCommentRequest request)
        {
            var command = new CloseSubscriptionCommand(id, request.Comment);
            var result = await _mediator.Send(command);

            if (!result)
                return BadRequest("Failed to close subscription.");

            return Ok();
        }
    }
}
