using FluentValidation;
using VOLXYSEAT.DOMAIN.Repositories;

namespace VOLXYSEAT.API.Application.Commands.Subscription.Update
{
    public class UpdateSubscriptionCommandValidator: AbstractValidator<UpdateSubscriptionCommand>
    {
        public UpdateSubscriptionCommandValidator(ISubscriptionRepository repository)
        {

            RuleFor(x => x.TypeId)
                .IsInEnum().WithMessage("Invalid TypeId")
                .NotEmpty().WithMessage("TypeId is required");

            RuleFor(x => x.StatusId)
                .IsInEnum().WithMessage("Invalid StatusId")
                .NotEmpty().WithMessage("StatusId is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(255).WithMessage("Description must be less than 255 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.UpdatedOn)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("UpdatedOn cannot be in the future");
        }
    }
}
