using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Extensions
{
    public static class DbExtention
    {
        public static ContentManager GetContentByTitle(this ApplicationDbContext context,string Title)
        {
            return context.CMS.Where(c => c.Title == Title).FirstOrDefault();
        }
    }
}
