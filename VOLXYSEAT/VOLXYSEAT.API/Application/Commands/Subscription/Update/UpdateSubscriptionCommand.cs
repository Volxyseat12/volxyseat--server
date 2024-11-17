using MediatR;
using VOLXYSEAT.API.Application.Models.Dtos.Subscription;
using VOLXYSEAT.DOMAIN.Models;

namespace VOLXYSEAT.API.Application.Commands.Subscription.Update
{
    public record UpdateSubscriptionCommand
    (
        Guid Id,
        SubscriptionEnum TypeId,
        SubscriptionStatus StatusId,
        string Description,
        decimal Price,
        string MercadoPagoPlanId,
        DateTime UpdatedOn,
        SubscriptionPropertiesDto SubscriptionProperties) : IRequest<bool>;
}
