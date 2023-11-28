using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class UserTimesheet
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long UserId { get; set; }
        public long CustomerId { get; set; }
        public long ProjectId { get; set; }
        public long ModuleId { get; set; }
        public string TaskDetails { get; set; }
        public double HoursWorked { get; set; }
        public double WorkingCost { get; set; }
        public string ApprovalStatus { get; set; }
        public long? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovalComments { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual Client Client { get; set; }
        public virtual ClientCustomers Customer { get; set; }
        public virtual ProjectModules Module { get; set; }
        public virtual ClientProjects Project { get; set; }
        public virtual Users User { get; set; }
    }
}
