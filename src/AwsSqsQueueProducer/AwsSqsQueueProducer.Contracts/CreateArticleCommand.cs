using MediatR;

namespace AwsSqsQueueProducer.Contracts;

public class CreateArticleCommand : IRequest<bool>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public List<AuthorCommandModel> Authors { get; set; } = new List<AuthorCommandModel>();
    public List<PublisherCommandModel> Publishers { get; set; } = new List<PublisherCommandModel>();
}

public class AuthorCommandModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}

public class PublisherCommandModel
{
    public string Name { get; set; }
    public DateTime DatePublished { get; set; }
}