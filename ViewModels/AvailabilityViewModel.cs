using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.ViewModels
{
    public class AvailabilityViewModel
    {
        [Key]
        public long Id { get; set; }
        public int? ClientId { get; set; }
        public int? CategoryId { get; set; }
        public long? TimingId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string StartTimeAmPm { get; set; }
        public string EndTimeAmPm { get; set; }
        public bool? IsAvailable { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; }

    }
}
