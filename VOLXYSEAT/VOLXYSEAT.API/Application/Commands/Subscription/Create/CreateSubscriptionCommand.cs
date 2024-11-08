using MediatR;
using System;
using VOLXYSEAT.API.Application.Models.Dtos.Subscription;
using VOLXYSEAT.DOMAIN.Models;

namespace VOLXYSEAT.API.Application.Commands.Subscription.Create
{
    public record class CreateSubscriptionCommand(
        SubscriptionEnum TypeId,
        SubscriptionStatus StatusId,
        string Description,
        decimal Price,
        DateTime CreatedOn,
        string MercadoPagoPlanId,
        DateTime UpdatedOn,
        SubscriptionPropertiesDto SubscriptionProperties) : IRequest<bool>;
}
