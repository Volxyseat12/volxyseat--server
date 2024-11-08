using VOLXYSEAT.DOMAIN.Models;

namespace VOLXYSEAT.API.Application.Models.Requests.Subscription
{
    public class SubscriptionRequest
    {
        public SubscriptionEnum Type { get; set; }
        public SubscriptionStatus Status { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
