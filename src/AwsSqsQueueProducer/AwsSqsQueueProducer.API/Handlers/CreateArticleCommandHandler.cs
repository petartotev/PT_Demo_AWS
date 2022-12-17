using AwsSqsQueueProducer.API.Services.Interfaces;
using AwsSqsQueueProducer.Contracts;
using MediatR;
using System.Text.Json;

namespace AwsSqsQueueProducer.API.Handlers
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, bool>
    {
        private readonly IAwsSqsQueueService _awsSqsQueueService;

        public CreateArticleCommandHandler(IAwsSqsQueueService awsSqsQueueService)
        {
            _awsSqsQueueService = awsSqsQueueService;
        }

        public async Task<bool> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var body = JsonSerializer.Serialize(command);

                Console.WriteLine(
                    "CreateArticleCommandHandler serialized command and is publishing it through IAwsSqsQueueService...");

                return await _awsSqsQueueService.PublishToAwsSqsQueueAsync(body);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                throw;
            }
        }
    }
}
