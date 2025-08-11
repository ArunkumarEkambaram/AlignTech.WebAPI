using System.ComponentModel.DataAnnotations;

namespace AlignTech.WebAPI.DataFirst.Models
{
    public class UserSubscription
    {
        public int Id { get; set; }

        [Required]
        public SubscriptionPlan SubscriptionPlan { get; set; } = SubscriptionPlan.Free;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddMonths(1);
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }

    public enum SubscriptionPlan
    {
        Free = 0,
        Basic,
        Standard,
        Premium
    }
}
