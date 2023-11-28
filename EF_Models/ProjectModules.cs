using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class ProjectModules
    {
        public ProjectModules()
        {
            UserTimesheet = new HashSet<UserTimesheet>();
        }

        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleDesc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual ClientProjects Project { get; set; }
        public virtual ICollection<UserTimesheet> UserTimesheet { get; set; }
    }
}
