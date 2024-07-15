namespace MessageService.Models.Context
{
    public abstract class MessageData
    {
        public IEnumerable<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; } = string.Empty;
    }
}
