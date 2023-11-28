using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class ClientCustomers
    {
        public ClientCustomers()
        {
            ClientProjects = new HashSet<ClientProjects>();
            UserProjectMapping = new HashSet<UserProjectMapping>();
            UserTimesheet = new HashSet<UserTimesheet>();
        }

        public long Id { get; set; }
        public long ClientId { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public bool? Enabled { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<ClientProjects> ClientProjects { get; set; }
        public virtual ICollection<UserProjectMapping> UserProjectMapping { get; set; }
        public virtual ICollection<UserTimesheet> UserTimesheet { get; set; }
    }
}
