using AwsSqsQueueConsumerLambda.Contracts;
using System.Text;

namespace AwsSqsQueueConsumerLambda.Services.Interfaces;

public interface IAwsEmailService
{
    public bool SendAwsEmail(string subject, string body, string receiver);

    public string CreateEmailContentOutOfArticleMessage(CreateArticleMessage message)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"Title:\n{message.Title}");
        stringBuilder.AppendLine($"Description:\n{message.Description}");
        stringBuilder.AppendLine($"Date:\n{message.DateCreated}");

        stringBuilder.AppendLine($"Authors:");
        foreach (var author in message.Authors)
        {
            stringBuilder.AppendLine($"{author.FirstName} {author.LastName} ({author.Age} years old)");
        }

        stringBuilder.AppendLine($"Publishers:");
        foreach (var publisher in message.Publishers)
        {
            stringBuilder.AppendLine($"{publisher.Name} (published in {publisher.DatePublished})");
        }

        stringBuilder.AppendLine($"Content:\n{message.Content}");

        return stringBuilder.ToString();
    }
}
