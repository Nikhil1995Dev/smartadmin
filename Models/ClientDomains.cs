using System;

namespace SmartAdmin.WebUI.Models
{
    public partial class ClientDomains
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public string DomainName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public byte? IsActive { get; set; }
        public byte? IsDeleted { get; set; }

        public virtual Client Client { get; set; }
    }
}
