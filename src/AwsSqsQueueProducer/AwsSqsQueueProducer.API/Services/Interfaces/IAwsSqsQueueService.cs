namespace AwsSqsQueueProducer.API.Services.Interfaces;

public interface IAwsSqsQueueService
{
    public Task<bool> PublishToAwsSqsQueueAsync(string body);
}
