using MediatR;
using VOLXYSEAT.DOMAIN.Exceptions;
using VOLXYSEAT.DOMAIN.Models;
using VOLXYSEAT.DOMAIN.Repositories;

namespace VOLXYSEAT.API.Application.Commands.Subscription.Update
{
    public class UpdateSubscriptionCommandHandler : IRequestHandler<UpdateSubscriptionCommand, bool>
    {
        private readonly ISubscriptionRepository _repository;

        public UpdateSubscriptionCommandHandler(ISubscriptionRepository repository)
        {
            _repository = repository ?? throw new VolxyseatDomainException(nameof(repository)); 
        }
        public async Task<bool> Handle(UpdateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new VolxyseatDomainException(nameof(request));

            var subscription = await _repository.GetByIdAsync(request.Id);

            if (subscription == null) throw new VolxyseatDomainException(nameof(subscription));

            subscription.UpdateDetails(
                request.TypeId,
                request.StatusId,
                request.Description,
                request.Price,
                request.MercadoPagoPlanId,
                DateTime.UtcNow
            );

            var subscriptionPropertiesDto = request.SubscriptionProperties;
            subscription.SubscriptionProperties = new SubscriptionProperties(
                subscription.Id,
                subscriptionPropertiesDto.Support,
                subscriptionPropertiesDto.Phone,
                subscriptionPropertiesDto.Email,
                subscriptionPropertiesDto.Messenger,
                subscriptionPropertiesDto.Chat,
                subscriptionPropertiesDto.LiveSupport,
                subscriptionPropertiesDto.Documentation,
                subscriptionPropertiesDto.Onboarding,
                subscriptionPropertiesDto.Training,
                subscriptionPropertiesDto.Updates,
                subscriptionPropertiesDto.Backup,
                subscriptionPropertiesDto.Customization,
                subscriptionPropertiesDto.Analytics,
                subscriptionPropertiesDto.Integration,
                subscriptionPropertiesDto.APIAccess,
                subscriptionPropertiesDto.CloudStorage,
                subscriptionPropertiesDto.MultiUser,
                subscriptionPropertiesDto.PrioritySupport,
                subscriptionPropertiesDto.SLA,
                subscriptionPropertiesDto.ServiceLevel
            );

            await _repository.UpdateAsync(subscription);
            var result = await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}
