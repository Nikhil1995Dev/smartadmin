using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class UserProjectMapping
    {
        public long Id { get; set; }
        public long? ClientId { get; set; }
        public long? CustomerId { get; set; }
        public long? ProjectId { get; set; }
        public long? UserId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual Client Client { get; set; }
        public virtual ClientCustomers Customer { get; set; }
        public virtual ClientProjects Project { get; set; }
        public virtual Users User { get; set; }
    }
}
