using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.ViewModels;

namespace SmartAdmin.WebUI.Controllers
{
    public class HomeLabTestController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public HomeLabTestController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName");
            return View();
        }
        public IActionResult GetOtherBookingByClientId(long? clientId,string category)
        {

            var model = context.OherBookings.Include(e => e.User).Include(e=>e.Category).Where(c => c.User.ClientId == clientId && c.Category.Code==category).ToList();
            var bookings = mapper.Map<List<OherBookingViewModel>>(model);
            var appointmentlisthtml = this.RenderViewToStringAsync("_OtherBookingByClientId", bookings.OrderByDescending(o => o.CreatedOn), true).Result;
            return Ok(appointmentlisthtml);
        }
    }
}
