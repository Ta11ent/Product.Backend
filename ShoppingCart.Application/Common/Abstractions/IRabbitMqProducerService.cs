namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IRabbitMqProducerService
    {
        Task SendProducerMessage(object data);
    }
}
