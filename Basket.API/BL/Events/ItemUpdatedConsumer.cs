using Basket.API.BO.Interfaces;
using MassTransit;
using Shared.BO.DTOs;

namespace Basket.API.BL.Events;

public class ItemUpdatedConsumer(IBasketService _basketService) : IConsumer<ItemUpdatedEvent>
{
    public async Task Consume(ConsumeContext<ItemUpdatedEvent> context)
    {
        await _basketService.Update(context.Message.Id, context.Message.Name, context.Message.Price);
    }
}
