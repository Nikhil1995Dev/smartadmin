using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAdmin.WebUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Controllers
{
    [Authorize]
    public class HraAssessmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HraAssessmentController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewBag.Clients = new SelectList(_context.Clients.ToList(), "Id", "ClientName");
            return View();
        }
        public IActionResult GetAssessmentByClient(long ClientId)
        {
            var assessments = _context.HraAssessments.Include(x=>x.User).Where(x => x.User.ClientId == ClientId).ToList();
            var assessmentshtml = this.RenderViewToStringAsync("_AssessmentByClientId", assessments, true).Result;
            return Ok(assessmentshtml);
        }
        public IActionResult GetAssessmentByUserId(Guid UserId)
        {
            var assessments = _context.HraAssessments.Where(u=>u.UserId==UserId).ToList();
            var assessmentshtml = this.RenderViewToStringAsync("_AssessmentByUserId", assessments, true).Result;
            return Ok(assessmentshtml);
        }

    }
}
