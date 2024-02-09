namespace MySqsQueuePublisher.API.Services.Interfaces
{
    public interface ISqsQueueService
    {
        public Task<bool> PublishToAwsSqsQueueAsync(string body);
    }
}
