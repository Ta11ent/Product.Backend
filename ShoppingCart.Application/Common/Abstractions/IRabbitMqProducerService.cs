namespace ShoppingCart.Application.Common.Abstractions
{
    public interface IRabbitMqProducerService
    {
        Task SendProducerMessage(Guid Id);
    }
}
