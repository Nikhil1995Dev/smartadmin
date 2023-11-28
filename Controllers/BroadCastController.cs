using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartAdmin.WebUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    [Authorize]
    public class BroadCastController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;

        public BroadCastController(ApplicationDbContext context, IMapper mapper)
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
