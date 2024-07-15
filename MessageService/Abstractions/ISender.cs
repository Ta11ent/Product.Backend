namespace MessageService.Abstractions
{
    public interface ISender<T> where T : class
    {
        void SendMessage(T data);
    }
}
