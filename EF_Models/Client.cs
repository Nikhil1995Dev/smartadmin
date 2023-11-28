using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class Client
    {
        public Client()
        {
            Branches = new HashSet<Branches>();
            ClientCustomers = new HashSet<ClientCustomers>();
            ClientProjects = new HashSet<ClientProjects>();
            Department = new HashSet<Department>();
            Roles = new HashSet<Roles>();
            UserProjectMapping = new HashSet<UserProjectMapping>();
            UserTimesheet = new HashSet<UserTimesheet>();
            Users = new HashSet<Users>();
        }

        public long Id { get; set; }
        public string Company { get; set; }
        public string ContactName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Subdomain { get; set; }
        public bool Enabled { get; set; }
        public long ThemeId { get; set; }
        public string AccountNo { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual ICollection<Branches> Branches { get; set; }
        public virtual ICollection<ClientCustomers> ClientCustomers { get; set; }
        public virtual ICollection<ClientProjects> ClientProjects { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }
        public virtual ICollection<UserProjectMapping> UserProjectMapping { get; set; }
        public virtual ICollection<UserTimesheet> UserTimesheet { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
