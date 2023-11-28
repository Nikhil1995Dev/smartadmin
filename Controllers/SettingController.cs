using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.EF_Models;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SettingController(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName");
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Guid  userId,long clientId)
        {
            try
            {
                var res = (dynamic)null;
                var user = context.Users.Find(userId);
                user.ClientId = clientId;
              
                context.Entry(user).State = EntityState.Modified;
                int n = context.SaveChanges();
                if (n > 0)
                {
                    var employeeMaster =  context.EmployeeMasters.FirstOrDefault(e => e.ClientId == clientId && e.Email == user.Email);
                    if (employeeMaster == null)
                    {
                        string empcode = "Emp00" + context.EmployeeMasters.Max(u => u.Id);
                        employeeMaster = new EmployeeMaster();
                        employeeMaster.FirstName = user.FirstName;
                        employeeMaster.LastName = user.LastName;
                        employeeMaster.Email = user.Email;
                        employeeMaster.Gender = user.Gender;
                        employeeMaster.ClientId = user.ClientId;
                        employeeMaster.EmployeeCode = empcode;
                        context.Add(employeeMaster);
                        context.SaveChanges();
                    }
                    res = new { status = "success", message = "user updated successfully" };
                }
                else {
                    res = new { status = "error", message = "Unable to update user" };
                }
                return Json(res);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        public PartialViewResult GetSettingByClientId()
        {
            var model = mapper.Map<List<UserViewModel>>(context.Users.Where(c => c.ClientId == 0 || c.ClientId == null).ToList());
            return PartialView("_SettingList", model);
        }
    }
}
