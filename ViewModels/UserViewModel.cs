
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.ViewModels
{
    public class UserViewModel
    {

        public Guid Id { get; set; }
        public long? ClientId { get; set; }
        public int AccessFailedCount { get; set; }
        
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }
        public long? LoginCount { get; set; }
        public string UserPhoto { get; set; }

        public Guid? UserTypeId { get; set; }
        public int? LoginAttempts { get; set; }
       
        public string PasswordHash { get; set; }
       
        public ClientViewModel Client { get; set; }
    }
}
