using SmartAdmin.WebUI.Models;
using System;

namespace SmartAdmin.WebUI.ViewModels
{
    public partial class AppointmentsViewModel
    {
        public long Id { get; set; }
        public long? ClientId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? AppointmentDate { get; set; }

        public DateTime? CreatedOn { get; set; }
        public TimeSpan? AppointMentFrom { get; set; }
        public TimeSpan? AppointMentTo { get; set; }
        public string AppointmentDateString { get; set; }
        public string AppointMentFromString { get; set; }
        public string AppointMentToString { get; set; }
        public string AppointMentType { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public long? AppointmentStatus { get; set; }
        public string AppointmentRemark { get; set; }
        public virtual User User { get; set; }
    }
}
