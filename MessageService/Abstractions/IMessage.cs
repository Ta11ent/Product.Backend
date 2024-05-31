using MessageService.Models.Context;

namespace MessageService.Abstractions
{
    public interface IMessage<T>
    {
        T CreateMessage(OrderDetailsDto data);
    }
}
