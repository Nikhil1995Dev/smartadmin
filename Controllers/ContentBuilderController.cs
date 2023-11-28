using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Extensions;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    [Authorize]
    public class ContentBuilderController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ContentBuilderController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public IActionResult Index(long? clientId)
        {
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName");
            return View();
        }
        public PartialViewResult Contents(long? clientId)
        {
            var contents = mapper.Map<List<CMSViewModel>>(context.CMS.Where(c => c.IsActive == true && c.ClientId == clientId).ToList());
            return PartialView("_ContentList",contents);
        }
        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CMSViewModel cMSViewModel)
        {
            try
            {
                var model = mapper.Map<ContentManager>(cMSViewModel);
                model.CreatedBy = User.GetUserId();
                model.CreatedDate = DateTime.Now;
                model.IsActive = true;
                context.CMS.Add(model);
                int n = context.SaveChanges();
                if (n > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { message = "Unable to create content" });

                }
            }
            catch(Exception e)
            {
                return BadRequest(new { message = e.Message});
            }
            
        }
        public IActionResult Edit(long? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var content =mapper.Map<CMSViewModel>(context.CMS.Find(id));
            ViewBag.Clients = new SelectList(context.Clients.ToList(), "Id", "ClientName", content.ClientId);
            return View(content);
        }
        [HttpPost]
        public IActionResult Edit(CMSViewModel cMSViewModel)
        {
            try
            {
                var model = mapper.Map<ContentManager>(cMSViewModel);
                model.ModifiedBy = User.GetUserId();
                model.ModifiedDate = DateTime.Now;
                context.CMS.Update(model);
                int n = context.SaveChanges();
                if (n > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { message = "Unable to update content" });

                }
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

        }
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var content = context.CMS.Find(id);
                content.IsActive = false;
                context.Entry(content).State =EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

        }

        public IActionResult GetContentById(long id)
        {
            try
            {
                var content = context.CMS.Find(id);
               
                return Ok(content);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

        }
        [HttpPost]
        public IActionResult SaveCopiedContent(CMSViewModel model)
        {
            try
            {
                model.IsActive = true;
                model.CreatedBy = User.GetUserId();
                model.CreatedDate = DateTime.Now;
                context.CMS.Add(mapper.Map<ContentManager>(model));
                int n = context.SaveChanges();
                if(n==0)
                {
                    return BadRequest(new { message = "unable to save content" });
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

        }
    }
}
