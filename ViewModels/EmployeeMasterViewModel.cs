using SmartAdmin.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.ViewModels
{
    public class EmployeeMasterViewModel
    {
        [Key]
        public long Id { get; set; }
        public long? ClientId { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        public int? Age { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string DependantType { get; set; }
        public bool? IsDependent { get; set; }
        public bool? IsCovered { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CoverStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CoverEndDate { get; set; }
        public long? PlanId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public virtual Client Client { get; set; }
    }
}
