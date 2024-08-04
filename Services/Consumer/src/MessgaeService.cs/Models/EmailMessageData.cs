using System.Security.Cryptography;

namespace MessageService.Models
{
    public class EmailMessageData
    {
        public List<string> To { get; set; } 
        public List<string>? CC { get; set; } 
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
