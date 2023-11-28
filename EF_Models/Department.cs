using System;
using System.Collections.Generic;

namespace SmartAdmin.WebUI.EF_Models
{
    public partial class Department
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual Client Client { get; set; }
    }
}
