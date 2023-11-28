using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.ViewModels
{
    public class ClientViewModel
    {
        public long Id { get; set; }
        [StringLength(200)]
        public string GroupCode { get; set; }
        public string ClientName { get; set; }
        public string ClientOverView { get; set; }
        public string ClientLogo { get; set; }
        [StringLength(400)]
        public string WebSiteUrl { get; set; }
        [StringLength(200)]
        public string ContactPerson { get; set; }
        [StringLength(200)]
        public string ContactNumber { get; set; }
        [StringLength(200)]
        public string ContactEmail { get; set; }
        [StringLength(4000)]
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(4000)]
        public string Col1 { get; set; }
        [StringLength(4000)]
        public string Col2 { get; set; }
        [StringLength(4000)]
        public string Col3 { get; set; }
        [StringLength(4000)]
        public string Col4 { get; set; }
        [StringLength(4000)]
        public string Col5 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public string LogoFile { get; set; }
    }
}
