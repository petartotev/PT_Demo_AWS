namespace MySqsQueuePublisher.API.Contracts
{
    public class MayaRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
