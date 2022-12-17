using Amazon.SQS;
using Amazon.SQS.Model;
using AwsSqsQueueProducer.API.Services.Interfaces;

namespace AwsSqsQueueProducer.API.Services
{
    public class AwsSqsQueueService : IAwsSqsQueueService
    {
        private readonly IConfiguration _configuration;

        public AwsSqsQueueService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> PublishToAwsSqsQueueAsync(string body)
        {
            var sqsClient = new AmazonSQSClient();

            await SendMessage(sqsClient, _configuration.GetValue<string>("AwsSqsQueueUrl"), body);

            return true;
        }

        private static async Task SendMessage(IAmazonSQS sqsClient, string sqsUrl, string messageBody)
        {
            try
            {
                SendMessageResponse response = await sqsClient.SendMessageAsync(sqsUrl, messageBody);

                Console.WriteLine($"Message added to queue\n  {sqsUrl}");
                Console.WriteLine($"HttpStatusCode: {response.HttpStatusCode}");

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                throw;
            }
        }
    }
}
