using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public long RoleId { get; set; }
        public string RoleTitle { get; set; }
        public string RoleDescription { get; set; }
        public bool? IsActive { get; set; }
        public long ClientId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? HierarchicalOrder { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
