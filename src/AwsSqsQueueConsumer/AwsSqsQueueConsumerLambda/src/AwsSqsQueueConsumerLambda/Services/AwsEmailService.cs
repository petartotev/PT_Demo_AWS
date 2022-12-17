using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using AwsSqsQueueConsumerLambda.Services.Interfaces;

namespace AwsSqsQueueConsumerLambda.Services
{
    public class AwsEmailService : IAwsEmailService
    {
        private const string EmailSender = "petar@petartotev.net";

        public bool SendAwsEmail(string subject, string body, string receiver)
        {
            using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.EUCentral1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = EmailSender,
                    Destination = new Destination { ToAddresses = new List<string> { receiver } },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body { Text = new Content { Charset = "UTF-8", Data = body } }
                    }
                };

                try
                {
                    Console.WriteLine("Sending email using Amazon SES...");
                    var response = client.SendEmailAsync(sendRequest).GetAwaiter().GetResult();
                    Console.WriteLine("The email was sent successfully.");

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The email was not sent.");
                    Console.WriteLine("Error message: " + ex.Message);
                }

                return false;
            }
        }
    }
}
