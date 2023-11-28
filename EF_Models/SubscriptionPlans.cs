using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class SubscriptionPlans
    {
        public SubscriptionPlans()
        {
            Users = new HashSet<Users>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public long? SubscribedUsersCount { get; set; }
        public long? ValidityInDays { get; set; }
        public string CurrencyType { get; set; }
        public bool? IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
