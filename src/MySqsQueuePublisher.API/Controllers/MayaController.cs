using Microsoft.AspNetCore.Mvc;
using MySqsQueuePublisher.API.Contracts;
using MySqsQueuePublisher.API.Services.Interfaces;
using Newtonsoft.Json;

namespace MySqsQueuePublisher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MayaController : ControllerBase
{
    private readonly ISqsQueueService _sqsService;

    public MayaController(ISqsQueueService sqsService)
    {
        _sqsService = sqsService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(MayaRequest request)
    {
        await _sqsService.PublishToAwsSqsQueueAsync(JsonConvert.SerializeObject(request));

        return Ok();
    }
}
