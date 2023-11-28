using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    public class EnquiryController : Controller
    {
        private readonly ApplicationDbContext context;

        public EnquiryController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
       
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName");
            return View();
        }
        public IActionResult GetEnquiryByClientId(long? clientId)
        {

            var model = context.ContactUs.Include(e => e.User).Where(c=>c.User.ClientId==clientId).ToList();
            var appointmentlisthtml = this.RenderViewToStringAsync("_EnquiryByClientId", model.OrderByDescending(o => o.CreatedOn), true).Result;
            return Ok(appointmentlisthtml);
        }
    }
}
