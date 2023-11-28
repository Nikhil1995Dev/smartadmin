using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Extensions;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.Services;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IMailService mailService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AppointmentsController(ApplicationDbContext context, IMapper mapper,IMailService mailService, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.mailService = mailService;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName");
            
            return View();
        }
        public IActionResult GetAppointmentsByClientId(long? clientId)
        {
         
            var appointmentsList = new List<AppointmentsViewModel>();
            var appointments = mapper.Map<List<AppointmentsViewModel>>(context.Appointments.Include(u => u.User).Where(c => c.ClientId == clientId).ToList());
            var AppointmentStatus = context.Settings.Where(c => c.SettingType == "AppointMentStatus").ToList();
            foreach (var item in appointments)
            {
                item.AppointMentFromString = DateTime.Today.Add((TimeSpan)item.AppointMentFrom).ToString("hh:mm tt");
                item.AppointMentToString = DateTime.Today.Add((TimeSpan)item.AppointMentTo).ToString("hh:mm tt");
                appointmentsList.Add(item);
                
            }
            ViewBag.ResponseStatus = new SelectList(AppointmentStatus, "Id", "SettingValue");
            var appointmentlisthtml =this.RenderViewToStringAsync("_AppointmentsByClientId", appointmentsList.OrderByDescending(o=>o.CreatedOn), true).Result;
            return Ok(appointmentlisthtml);
        }
        public IActionResult UpdateAppointmentStatus(AppointmentsViewModel model)
        {
            try
            {
                
                var appointment = context.Appointments.Find(model.Id);
                var emailId = context.Users.Find(appointment.UserId).Email;
                appointment.AppointmentStatus = model.AppointmentStatus;
                appointment.AppointmentRemark = model.AppointmentRemark;
                appointment.UpdatedBy = User.GetUserId();
                appointment.UpdatedOn = DateTime.Now;
                context.Entry(appointment).State = EntityState.Modified;
                int n = context.SaveChanges();
                if (n > 0)
                {
                    var appstatus = context.Settings.Find(model.AppointmentStatus).SettingValue;
                    if (appstatus == "Cancelled")
                    {
                        var availability = context.Availabilities.FirstOrDefault(c => c.StartTime == appointment.AppointMentFrom && c.EndTime == appointment.AppointMentTo && c.StartDate == appointment.AppointmentDate && c.ClientId == appointment.ClientId && c.CategoryId == appointment.CategoryId);
                        availability.IsBooked = false;
                        context.Entry(availability).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    ////var mailMessage = new MailRequest { ToEmail = emailId, Subject = "Appointment Status", Body = "Your appointment has " + appstatus };
                    ////mailService.SendEmailAsync(mailMessage);
                    return Ok("Appointment status updated successfully");
                }
                else
                {
                    return Ok("Unable to update appointment status");
                }
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
       [HttpPost]
         public async Task<IActionResult> UploadDocument(AppointmentDocumentViewModel model)
        {
            try
            {
                if (model.Files != null)
                {
                    List<AppointmentDocument> documents = new List<AppointmentDocument>();
                  
                        foreach(var item in model.Files)
                        {
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                            string res =await UploadFile(item, fileName);
                            if (res == "Ok")
                            {
                                AppointmentDocument appointmentDocument = new AppointmentDocument();
                                appointmentDocument.CreatedBy = IdentityExtensions.GetUserId(this.User);
                                appointmentDocument.FileName = fileName;
                                appointmentDocument.AppointmentId = model.AppointmentId;
                                appointmentDocument.UploadedOn = DateTime.Now;
                                documents.Add(appointmentDocument);

                            }
                            else
                            {
                                return BadRequest(res);
                            }
                      
                        }
                    await context.AppointmentDocuments.AddRangeAsync(documents);
                    int dbRes = await context.SaveChangesAsync();
                    if (dbRes > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Unable to save documents");
                    }


                }
                else
                {
                    return BadRequest("Please choose  documents");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
        public async Task<IActionResult> GetAllDocuments(long appointmentid)
        {
            try
            {
                var res = await context.AppointmentDocuments.Where(a => a.AppointmentId == appointmentid).Select(s => new { name = s.FileName }).ToArrayAsync();
                return Ok(res);
            }
          catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        private async Task<string> UploadFile(IFormFile file, string FileName)
        {
            try
            {

                string path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads/AppointmentDocument/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                await   file.CopyToAsync(new FileStream(path + FileName, FileMode.Create));
                return "Ok";

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
