using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.ViewModels
{
    public class ImageGallaryViewModel
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Display(Name ="Image")]
        public string ImageName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        [Required]
        public IFormFile file { get; set; }
    }
}
