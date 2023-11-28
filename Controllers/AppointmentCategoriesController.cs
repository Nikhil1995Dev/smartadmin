using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartAdmin.WebUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    public class AppointmentCategoriesController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;

        public AppointmentCategoriesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var clients = _context.Clients.ToList();
            ViewBag.Clients = new SelectList(clients, "Id", "ClientName");
            return View();
        }
    }
}
