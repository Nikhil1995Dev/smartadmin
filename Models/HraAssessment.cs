﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAdmin.WebUI.Models
{
    [Table("HraAssessment")]
    public partial class HraAssessment
    {
        [Key]
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Score { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string Col1 { get; set; }
        public string Col2 { get; set; }
        public string Col3 { get; set; }
        public string Col4 { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("HraAssessments")]
        public virtual User User { get; set; }
    }
}