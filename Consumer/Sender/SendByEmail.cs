namespace Consumer.Recipient
{
    internal class SendByEmail : ISender
    {
        public void SendToRecipient(object message)
        {
            Console.WriteLine(message.ToString());
        }
    }
}
