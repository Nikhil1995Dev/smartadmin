﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAdmin.WebUI.Models
{
    [Table("ContentManager")]
    public partial class ContentManager
    {
        [Key]
        public long Id { get; set; }
        public long? ClientId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("ContentManagers")]
        public virtual Client Client { get; set; }
    }
}