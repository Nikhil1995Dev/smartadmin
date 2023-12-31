﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.AspNetCore.Http;
using SmartAdmin.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAdmin.WebUI.ViewModels
{

    public  class AppointmentDocumentViewModel
    {
        [Key]
        public long Id { get; set; }
        public long? AppointmentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UploadedOn { get; set; }
        [StringLength(200)]
        public string FileName { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}