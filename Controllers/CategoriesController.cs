using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CategoriesController(ApplicationDbContext context,IMapper mapper,IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Details(long id)
        {
            try
            {
                var category = mapper.Map<CategoryViewModel>(context.Categories.Find(id));
                return PartialView("Details", category);
            }
            catch
            {
                throw;
            }
        }
        [HttpGet]
       public PartialViewResult Edit(long id)
        {
            try
            {
                var category =mapper.Map<CategoryViewModel>(context.Categories.Find(id));
                return PartialView("Edit", category);
            }
            catch
            {
                throw;
            }
        }
     
        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            try
            {
                var category = mapper.Map<Category>(model);
                if (model.file != null)
                {
                    if ((model.file.Length / 1024f) / 1024f > 5f)
                    {
                        return BadRequest("Category image can'nt greater than 2mb");
                    }

                    string uniqueFileName = Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(model.file.FileName);
                    bool uploadStatus = UploadFile(model.file, uniqueFileName);
                    if (uploadStatus)
                    {
                        category.CatImage = uniqueFileName;
                    }
                }
                category.UpdatedBy = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                category.UpdatedDate = DateTime.Now;
                context.Categories.Update(category);
                context.SaveChanges();
                return Ok();
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
            try
            {
                var category = mapper.Map<Category>(model);
                if (model.file != null)
                {  
                    if((model.file.Length/ 1024f) / 1024f>5f)
                    {
                        return BadRequest("Category image can'nt greater than 2mb");
                    }

                    string uniqueFileName = Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(model.file.FileName);
                    bool uploadStatus = UploadFile(model.file, uniqueFileName);
                    if(uploadStatus)
                    {
                        category.CatImage = uniqueFileName;
                    }
                }
                category.CreatedBy =Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                category.CreatedDate = DateTime.Now;
                context.Categories.Add(category);
                context.SaveChanges();
                return Ok();
            }
            catch
            {
                throw;
            }
        }

        private bool UploadFile(IFormFile file,string uniqueFileName)
        {
            try
            {
               
                string path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads/Category/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                file.CopyToAsync(new FileStream(path+uniqueFileName, FileMode.Create));
                return true;

            }
            catch
            {
                return false;
            }
     
        }
        [HttpGet]
        public ActionResult Delete(long id)
        {
            try
            {
                var category = context.Categories.Find(id);
                category.UpdatedBy = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                category.UpdatedDate = DateTime.Now;
                category.IsActive = false;
                context.Entry<Category>(category).State = EntityState.Modified;
                int n = context.SaveChanges();
                if (n > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { message = "Unable to delete category.." });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new { message = e.Message});
            }
        }

    }
}
