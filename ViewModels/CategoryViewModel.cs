using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public long Id { get; set; }
        [StringLength(400)]
        [Display(Name = "Category Name")]
        [Required(ErrorMessage ="Please enter category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Category Description")]
        public string CategoryDesc { get; set; }
        [StringLength(4000)]
        [Display(Name = "Category Image")]
        public string CatImage { get; set; }
        [StringLength(100)]
        public string ActionType { get; set; }
        public long? ParentId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(4000)]
        public string TmpImage { get; set; }
        [StringLength(4000)]
        [Display(Name = "Category Keyword")]
        public string CategoryKeyword { get; set; }
        [Display(Name = "Category Sequence")]
        public int? CatSeq { get; set; }
        public IFormFile file { get; set; }
    }
}
