using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    public class QuestionController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
