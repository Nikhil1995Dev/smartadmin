using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class EmployeeMasterController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EmployeeMasterController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        // GET: EmployeeMasterController
        public ActionResult Index()
        {

            return View();
        }


        // GET: EmployeeMasterController/Details/5
      
        public async Task<ActionResult> GetEmployeeByClientId(long id)
        {
            var employees = await context.EmployeeMasters.Where(c => c.ClientId == id).ToListAsync();

            return Ok(new { data = employees, recordsTotal = employees.Count, recordsFiltered = employees.Count });
        }

        // GET: EmployeeMasterController/Create
        public ActionResult Create()
        {
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName");
            return View();
        }

        // POST: EmployeeMasterController/Create
        [HttpPost]
        public async Task<ActionResult> Create(List<EmployeeMasterViewModel> model)
        {
            try
            {
                var employees = context.EmployeeMasters.Where(x => x.ClientId == model.ElementAt(0).ClientId).AsQueryable();

                if (employees.Any())
                {
                    List<EmployeeMasterArchieve> employeeMasterArchieves = new List<EmployeeMasterArchieve>();
                    foreach (var item in employees)
                    {
                        employeeMasterArchieves.Add(new EmployeeMasterArchieve
                        {
                            Age = item.Age,
                            ClientId = item.ClientId,
                            CoverEndDate = item.CoverEndDate,
                            CoverStartDate = item.CoverStartDate,
                            CreatedBy = item.CreatedBy,
                            ArchieveDate = DateTime.Now,
                            CreatedDate = item.CreatedDate,
                            DependantType = item.DependantType,
                            Email = item.Email,
                            EmployeeCode = item.EmployeeCode,
                            FirstName = item.FirstName,
                            Gender = item.Gender,
                            Id = item.Id,
                            IsActive = item.IsActive,
                            IsCovered = item.IsCovered,
                            IsDependent = item.IsDependent,
                            LastName = item.LastName,
                            ModifiedBy = item.ModifiedBy,
                            ModifiedDate = item.ModifiedDate,
                            PhoneNumber = item.PhoneNumber,
                            PlanId = item.PlanId,

                        });
                    }
                    await context.AddRangeAsync(employeeMasterArchieves);
                    int result = await context.SaveChangesAsync();
                    if (result > 0)
                    {
                        context.RemoveRange(employees);
                        var employeesList = mapper.Map<List<EmployeeMaster>>(model);
                        await context.AddRangeAsync(employeesList);
                      result=  await context.SaveChangesAsync();
                        if(result>0)
                        {
                            return Ok("Record saved successfully");
                        }
                        else
                        {
                            return BadRequest(new { message = "Unable to save record" });
                        }
                    }
                    else
                    {
                        return BadRequest(new { message = "Unable to save record" });
                    }
                }
                else
                {
                    var employeesList = mapper.Map<List<EmployeeMaster>>(model);
                    context.EmployeeMasters.AddRange(employeesList);
                    int n = await context.SaveChangesAsync();
                    if (n > 0)
                    {
                        return Ok("Record saved successfully");
                    }
                    else
                    {
                        return BadRequest(new { message = "Unable to save record" });
                    }
                }
            }
            catch(Exception e)
            {

                return BadRequest(new { message = e.Message });
            }
        }

        // GET: EmployeeMasterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeMasterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeMasterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeMasterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
