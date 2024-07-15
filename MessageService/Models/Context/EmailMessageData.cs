namespace MessageService.Models.Context
{
    public class EmailMessageData : MessageData
    {
       // public EmailMessageData() { }
        //public EmailMessageData(
        //    IEnumerable<string> to,
        //    IEnumerable<string>? cc,
        //    string subject,
        //    string body) {

        //    To = to;//
        //    CC = cc;
        //    Subject = subject;
        //    Body = body;
        //}/
        public IEnumerable<string>? CC { get; private set; }
    }
}

