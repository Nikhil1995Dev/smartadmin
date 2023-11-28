using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Models
{
    public class DriverDocumentUpload
    {
        
        public IFormFile AdharBack { get; set; }
      
        public IFormFile AdharFront { get; set; }
        public IFormFile LicenseFront { get; set; }

        public IFormFile LicenseBack { get; set; }
        public IFormFile ProfilePic { get; set; }

        public IFormFile Vehicle1 { get; set; }
        public IFormFile Vehicle2 { get; set; }

        public IFormFile Vehicle3 { get; set; }
        public IFormFile Vehicle4 { get; set; }

        public IFormFile DrivingLicenseFile { get; set; }
    }
}
