using AwsSqsQueueProducer.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AwsSqsQueueProducer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleCommand command)
        {
            Console.WriteLine(
                $"Controller received CreateArticleCommand with title '{command.Title}' and is sending it to Handler...");

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
