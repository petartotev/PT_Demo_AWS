using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using AwsSqsQueueConsumerLambda.Contracts;
using AwsSqsQueueConsumerLambda.Services;
using AwsSqsQueueConsumerLambda.Services.Interfaces;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AwsSqsQueueConsumerLambda;

public class Function
{
    private readonly IAwsEmailService _awsEmailService;

    public Function()
    {
        _awsEmailService = new AwsEmailService();
    }

    public string FunctionHandler(SQSEvent sqsEvent, ILambdaContext context)
    {
        Console.WriteLine($"Beginning to process {sqsEvent.Records.Count} records...");

        foreach (var record in sqsEvent.Records)
        {
            if (string.IsNullOrWhiteSpace(record.Body))
            {
                continue;
            }

            Console.WriteLine($"Message ID: {record.MessageId}");
            Console.WriteLine($"Event Source: {record.EventSource}");
            Console.WriteLine($"Record Body:\n{record.Body}");

            var createArticleMessage = JsonSerializer.Deserialize<CreateArticleMessage>(record.Body);

            Console.WriteLine("Record body successfully deserialized to CreateArticleMessage.");

            var content = _awsEmailService.CreateEmailContentOutOfArticleMessage(createArticleMessage);

            Console.WriteLine("Content for the email successfully generated!");
            Console.WriteLine($"Content:\n{content}");

            try
            {
                _awsEmailService.SendAwsEmail(createArticleMessage.Title, content, "petar@petartotev.net");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR! " + ex.Message);
                throw;
            }
        }

        Console.WriteLine("Processing complete.");

        return $"Processed {sqsEvent.Records.Count} records.";
    }
}
