﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAdmin.WebUI.Models
{
    public partial class ContactU
    {
        [Key]
        public long Id { get; set; }
        public Guid? UserId { get; set; }
        public string Message { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("ContactUs")]
        public virtual User User { get; set; }
    }
}