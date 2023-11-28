using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Models
{      
    public class RidesSearchModel
    {
        //public long Id { get; set; }
        //public long? UserId { get; set; }

        public string UserType { get; set; }
        public string UserName { get; set; }
        public string UserMobileNumber { get; set; }
        //public long? DriverId { get; set; }
        //public double? TotalAmount { get; set; }
        public string IsOngoing { get; set; }
        public string RideStatus { get; set; }
        //public double? Ratings { get; set; }
        //public string ReviewComment { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        //public string PaymentType { get; set; }
        //public int? AttemptCount { get; set; }
        public string IsActive { get; set; }
        //public long? CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public long? UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }
    }
}
