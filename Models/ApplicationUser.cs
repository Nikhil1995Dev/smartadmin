using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Models
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string Name { get; set; }
        public long? ClientId { get; set; }
    }
    public class ApplicationRole : IdentityRole<long>
    {

    }
}
