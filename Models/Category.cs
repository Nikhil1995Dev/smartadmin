﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAdmin.WebUI.Models
{
    [Table("Category")]
    public partial class Category
    {
        [Key]
        public long Id { get; set; }
        [StringLength(400)]
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        [StringLength(4000)]
        public string CatImage { get; set; }
        [StringLength(100)]
        public string ActionType { get; set; }
        public long? ParentId { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(4000)]
        public string TmpImage { get; set; }
        [StringLength(4000)]
        public string CategoryKeyword { get; set; }
        public int? CatSeq { get; set; }
    }
}