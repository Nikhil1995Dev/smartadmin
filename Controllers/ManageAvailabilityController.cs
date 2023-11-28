      using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    public class ManageAvailabilityController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ManageAvailabilityController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName");
            return View();
        }
        public ActionResult GetTimeSlot(string StartDate, int ClientId,int Category)
        {
            try
            {
                DateTime myDate;
                if (!DateTime.TryParse(StartDate, out myDate))
                {
                    return BadRequest();
                }
                List<AvailabilityViewModel> lstAvailability = new List<AvailabilityViewModel>();
                var availabilities =mapper.Map<List<AvailabilityViewModel>>(context.Availabilities.Where(a => a.ClientId == ClientId && a.StartDate == myDate && a.CategoryId==Category).OrderBy(o => o.StartTime).ToList());
                foreach(var item in availabilities)
                {
                    item.StartTimeAmPm = DateTime.Today.Add((TimeSpan)item.StartTime).ToString("hh:mm tt");
                    item.EndTimeAmPm = DateTime.Today.Add((TimeSpan)item.EndTime).ToString("hh:mm tt");
                    lstAvailability.Add(item);
                }
                return Ok(availabilities);
            }
            catch
            {
                throw;
            }
        }
        public ActionResult CreateSlot(string Day, int ClientId,int Category, string StartDate)
        {
            try
            {

                DateTime _StartDate;
                int slotduration = 0;
                if (!DateTime.TryParse(StartDate, out _StartDate))
                {
                    return BadRequest(new { message="Invalide date"});
                }
                var timing = context.Timings.Where(a => a.ClientId == ClientId && a.Day == Day).FirstOrDefault();

                slotduration = (int)context.AppoinmentCategories.FirstOrDefault(s => s.ClientId == ClientId && s.Id==Category).SlotDuration;
                if(slotduration == 0)
                {
                    return BadRequest(new { message = "set slot duration in for this category" });
                }
               
                
                if (!timing.MorningStart .HasValue)
                {
                    timing.MorningStart = new TimeSpan();
                }
                if (!timing.MorningEnd.HasValue)
                {
                    timing.MorningEnd = new TimeSpan();
                }
                if (!timing.EveningStart.HasValue)
                {
                    timing.EveningStart = new TimeSpan();
                }
                if (!timing.EveningEnd.HasValue)
                {
                    timing.EveningEnd = new TimeSpan();
                }
                int MorningTime = (int)timing.MorningEnd.Value.Subtract(timing.MorningStart.Value).TotalMinutes;
                int EveningTime = (int)timing.EveningEnd.Value.Subtract(timing.EveningStart.Value).TotalMinutes;
                List<Availability> availabilities = new List<Availability>();
                TimeSpan startTimeMorrning = timing.MorningStart.Value;
                TimeSpan startTimeEvening = timing.EveningStart.Value;
                // loop for morning time
                if (MorningTime == 0 && EveningTime == 0)
                {
                    return BadRequest(new { message = "Set timing for " + Day });
                }

                while (MorningTime > 0)
                {
                    var availability = new Availability
                    {
                        CategoryId=Category,
                        ClientId = ClientId,
                        TimingId = timing.Id,
                        StartDate = _StartDate,
                        StartTime = startTimeMorrning,
                        EndTime = startTimeMorrning.Add(TimeSpan.FromMinutes(slotduration)),
                        IsAvailable = true,
                        IsActive = true,
                    };
                    if (MorningTime < slotduration)
                    {
                        //if(MorningTime>0)
                        //{
                           
                        //    availabilities.Add(new Availability {
                        //        ClientId = ClientId,
                        //    TimingId = timing.Id,
                        //    StartDate = _StartDate,
                        //    StartTime = availability.EndTime,
                        //    EndTime = availability.EndTime.Value.Add(TimeSpan.FromMinutes(MorningTime)),
                        //    IsAvailable = true,
                        //    IsActive = true,
                        //});
                        //}
                        MorningTime = 0;
                    }
                    availabilities.Add(availability);
                    startTimeMorrning = (TimeSpan)availability.EndTime;
                    MorningTime = MorningTime - slotduration;
                   

                }
                // loop for evening time

                while (EveningTime > 0)
                {
                    var availability = new Availability
                    {
                        CategoryId=Category,
                        ClientId = ClientId,
                        TimingId = timing.Id,
                        StartDate = _StartDate,
                        StartTime = startTimeEvening,
                        EndTime = startTimeEvening.Add(TimeSpan.FromMinutes(slotduration)),
                        IsAvailable = true,
                        IsActive = true,
                    };
                    availabilities.Add(availability);
                    startTimeEvening = (TimeSpan)availability.EndTime;
                    EveningTime = EveningTime - slotduration;
                    if (EveningTime < slotduration)
                    {
                        EveningTime = 0;
                    }
                }
                var CurrentDayAvailability= context.Availabilities.Where(a => a.StartDate == _StartDate  &&a.ClientId==ClientId && a.CategoryId==Category).AsQueryable();
                if(CurrentDayAvailability.Any())
                {
                    context.Availabilities.RemoveRange(CurrentDayAvailability.ToList());
                    context.SaveChanges();
                }
                context.Availabilities.AddRange(availabilities);
                int n = context.SaveChanges();
                if (n > 0)
                    return Ok();
                else
                    return BadRequest(new { message = "Unable to create slot" });

            }
            catch
            {
                return BadRequest(new { message = "internal server error" });
            }
        }

        public ActionResult ChangeAvailabilityStatus(long id)
        {
            try
            {
                var availability = context.Availabilities.Find(id);
                availability.IsAvailable = (bool)availability.IsAvailable ? false : true;
                context.Entry<Availability>(availability).State = EntityState.Modified;
                context.SaveChanges();
                return Ok(availability.IsAvailable);
            }
            catch
            {
                throw;
            }
        }
    }
}
