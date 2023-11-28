using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Extensions;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    public class ImageGallaryController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ImageGallaryController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }
        public PartialViewResult GetImageList()
        {
            try
            {
                var images = context.ImageGallary.Where(e => e.IsActive == true).ToList();
                return PartialView("_GetImageList", mapper.Map<List<ImageGallaryViewModel>>(images));
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(ImageGallaryViewModel gallaryViewModel)
        {
            try
            {
                if (gallaryViewModel.file != null)
                {
                    string fileName = gallaryViewModel.Title.Replace(' ', '-') + Path.GetExtension(gallaryViewModel.file.FileName);
                    string res = UploadFile(gallaryViewModel.file, fileName);
                    if (res == "Ok")
                    {
                        var gallaryModel = mapper.Map<ImageGallary>(gallaryViewModel);
                        gallaryModel.CreatedBy = IdentityExtensions.GetUserId(this.User);
                        gallaryModel.CreatedDate = DateTime.Now;
                        gallaryModel.IsActive = true;
                        gallaryModel.ImageName = fileName;
                        context.ImageGallary.Add(gallaryModel);
                        context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
                else
                {
                    return BadRequest("Please choose an image");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       
        }
        private string UploadFile(IFormFile file, string FileName)
        {
            try
            {

                string path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads/ImageGallary/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                file.CopyToAsync(new FileStream(path + FileName, FileMode.Create));
                return "Ok";

            }
            catch(Exception e)
            {
                return e.Message;
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
                var imagegal = context.ImageGallary.Find(id);
                context.Entry(imagegal).State = EntityState.Deleted;
               int n= context.SaveChanges();
                if(n>0)
                {
                    string directoryPath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads/ImageGallary/"+ imagegal.ImageName);
                    if (System.IO.File.Exists(directoryPath))
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        System.IO.File.Delete(directoryPath);
                    }

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
