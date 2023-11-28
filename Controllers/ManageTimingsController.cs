using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    public class ManageTimingsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ManageTimingsController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName");
            return View();
        }
        public async Task<ActionResult> GetTimingsByClientId(int clientId)
        {
            try
            {
                List<TimingViewModel> timingiList = new List<TimingViewModel>();
                var timings = await context.Timings.Include(c => c.Client).Where(c => c.ClientId == clientId).ToListAsync();
                var model = mapper.Map<List<TimingViewModel>>(timings);
                foreach(var item in model)
                {
                    if (item.MorningStart == null)
                    {
                        item.MorningStart = new TimeSpan();
                    }
                    if (item.MorningEnd == null)
                    {
                        item.MorningEnd = new TimeSpan();
                    }
                    if (item.EveningStart == null)
                    {
                        item.EveningStart = new TimeSpan();
                    }
                    if (item.EveningEnd == null)
                    {
                        item.EveningEnd = new TimeSpan();
                    }
                    item.Morning = DateTime.Today.Add((TimeSpan)item.MorningStart).ToString("hh:mm tt")+" - "+ DateTime.Today.Add((TimeSpan)item.MorningEnd).ToString("hh:mm tt");
                    item.Evening = DateTime.Today.Add((TimeSpan)item.EveningStart).ToString("hh:mm tt") + " - " + DateTime.Today.Add((TimeSpan)item.EveningEnd).ToString("hh:mm tt");
                   
                    timingiList.Add(item);
                   
                   
                }
                return Ok(timingiList);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        public IActionResult Edit(TimingViewModel model)
        {
            try
            {
                var res = (dynamic)null;
                TimeSpan MorningStart=Convert.ToDateTime(model.MorningStartTime).TimeOfDay;
                TimeSpan MorningEnd= Convert.ToDateTime(model.MorningEndTime).TimeOfDay;
                TimeSpan EveningStart= Convert.ToDateTime(model.EveningStartTime).TimeOfDay;
                TimeSpan EveningEnd= Convert.ToDateTime(model.EveningEndTime).TimeOfDay;
             
                var timing = context.Timings.Find(model.Id);
                timing.MorningStart = MorningStart;
                timing.MorningEnd = MorningEnd;
                timing.EveningStart =EveningStart;
                timing.EveningEnd = EveningEnd;
               // timing.ModifiedBy = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                timing.ModifiedDate = DateTime.Now;
                context.Entry(timing).State = EntityState.Modified;
                int n = context.SaveChanges();
                if (n > 0)
                {
                    res = new { status = "success", message = "Timing updated successfully" };
                }
                else
                {
                    res = new { status = "error", message = "Unable to update Timing" };
                }
                return Json(res);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
