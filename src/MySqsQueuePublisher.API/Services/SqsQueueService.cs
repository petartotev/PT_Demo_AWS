using Amazon.SQS;
using Amazon.SQS.Model;
using MySqsQueuePublisher.API.Services.Interfaces;

namespace MySqsQueuePublisher.API.Services;

public class SqsQueueService : ISqsQueueService
{
    private readonly IConfiguration _configuration;

    public SqsQueueService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> PublishToAwsSqsQueueAsync(string body)
    {
        var sqsClient = new AmazonSQSClient();

        await SendMessage(sqsClient, _configuration.GetValue<string>("MySqsQueueTestUrl"), body);

        return true;
    }

    private static async Task SendMessage(IAmazonSQS sqsClient, string sqsUrl, string messageBody)
    {
        try
        {
            SendMessageResponse response = await sqsClient.SendMessageAsync(sqsUrl, messageBody);

            Console.WriteLine($"Message added to queue {sqsUrl}");
            Console.WriteLine($"HttpStatusCode: {response.HttpStatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);
        }
    }
}
