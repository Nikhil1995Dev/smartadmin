using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    public class AnswerOptionsController : Controller
    {
        [Authorize]
        public IActionResult Index(long? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            return View();
        }
    }
}
