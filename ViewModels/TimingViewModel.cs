using SmartAdmin.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.ViewModels
{
    public class TimingViewModel
    {
        [Key]
        public long Id { get; set; }
        public int? ClientId { get; set; }
        [StringLength(50)]
        public string Day { get; set; }
        [StringLength(50)]
        public string MorningStartTime { get; set; }
        [StringLength(50)]
        public string MorningEndTime { get; set; }
        [StringLength(50)]
        public string EveningStartTime { get; set; }
        [StringLength(50)]
        public string EveningEndTime { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public  List<Availability> Availabilities { get; set; }
        public string  ClientName { get; set; }
        public string Evening { get; set; }
        public string Morning { get; set; }
        public TimeSpan? MorningStart { get; set; }
        public TimeSpan? MorningEnd { get; set; }
        public TimeSpan? EveningStart { get; set; }
        public TimeSpan? EveningEnd { get; set; }


    }
}
