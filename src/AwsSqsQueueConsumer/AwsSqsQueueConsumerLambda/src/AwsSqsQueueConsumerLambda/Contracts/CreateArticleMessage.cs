namespace AwsSqsQueueConsumerLambda.Contracts;

public class CreateArticleMessage
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public List<AuthorMessageModel> Authors { get; set; } = new List<AuthorMessageModel>();
    public List<PublisherMessageModel> Publishers { get; set; } = new List<PublisherMessageModel>();
}

public class AuthorMessageModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

public class PublisherMessageModel
{
    public string Name { get; set; }
    public DateTime DatePublished { get; set; }
}
