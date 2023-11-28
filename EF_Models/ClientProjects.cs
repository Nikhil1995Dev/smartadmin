using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class ClientProjects
    {
        public ClientProjects()
        {
            ProjectModules = new HashSet<ProjectModules>();
            UserProjectMapping = new HashSet<UserProjectMapping>();
            UserTimesheet = new HashSet<UserTimesheet>();
        }

        public long Id { get; set; }
        public long ClientId { get; set; }
        public long CustomerId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDesc { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public double ProjectTotalCost { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual Client Client { get; set; }
        public virtual ClientCustomers Customer { get; set; }
        public virtual ICollection<ProjectModules> ProjectModules { get; set; }
        public virtual ICollection<UserProjectMapping> UserProjectMapping { get; set; }
        public virtual ICollection<UserTimesheet> UserTimesheet { get; set; }
    }
}
