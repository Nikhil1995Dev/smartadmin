using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Extensions;
using SmartAdmin.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.EndPoints
{
    [Route("api/apptcat")]
    [ApiController]
    public class AppointmentCategoriesEndPoint : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;

        public AppointmentCategoriesEndPoint(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("get-by-clientId/{ClientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AppoinmentCategory>> GetByClientId([FromRoute] long ClientId)
        {
            List<AppoinmentCategory> categories =_context.AppoinmentCategories.Where(c => c.ClientId == ClientId).ToList();
            return Ok(new { data = categories, recordsTotal = categories.Count, recordsFiltered = categories.Count });
        }
        [HttpGet]
        [Route("binddropdown-by-clientId/{ClientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AppoinmentCategory>> BindDropdownByClientId([FromRoute] long ClientId)
        {
            List<AppoinmentCategory> categories = _context.AppoinmentCategories.Where(c => c.ClientId == ClientId).ToList();
            var ddlData = categories.Select(x => new { id = x.Id, text = x.Name +" ("+x.Code+")"});
       
            return Ok(ddlData);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AppoinmentCategory> Get([FromRoute] long id)
        {
            AppoinmentCategory category = _mapper.Map<AppoinmentCategory>(_context.AppoinmentCategories.Find(id));
            return Ok(category);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public IActionResult Create([FromForm] AppoinmentCategory model)
        {

            model.CreatedOn = DateTime.Now;
            model.CreatedBy = User.GetUserId();
            model.Code = _context.AppointmentCategoryMasters.SingleOrDefault(x => x.Name == model.Name).Code;
            _context.AppoinmentCategories.Add(model);

            if (_context.SaveChanges() > 0)
            {
                return Ok(new { ResponseCode = "success", ResponseMessage = "Appoinment Category created successful!!!!!!!" });
            }
            else
            {

                return Ok(new { ResponseCode = "error", ResponseMessage = "Something went wrong." });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult Update([FromForm] AppoinmentCategory model)
        {

            model.UpdatedOn = DateTime.Now;
            model.UpdatedBy = User.GetUserId();
            model.Code = _context.AppointmentCategoryMasters.SingleOrDefault(x => x.Name == model.Name).Code;
            _context.AppoinmentCategories.Update(model);

            if (_context.SaveChanges() > 0)
            {
                return Ok(new { ResponseCode = "success", ResponseMessage = "Appoinment Category updated successfully" });
            }
            else
            {

                return Ok(new { ResponseCode = "error", ResponseMessage = "Something went wrong." });
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult Delete([FromForm] AppoinmentCategory model)
        {
            _context.Remove(model);
            if (_context.SaveChanges() > 0)
            {
                return Ok(new { ResponseCode = "success", ResponseMessage = "Appoinment Category deleted successfully" });
            }

            else
            {

                return Ok(new { ResponseCode = "error", ResponseMessage = "Something went wrong." });
            }
        }
        [HttpGet]
        [Route("get-all-cat")]
        public IActionResult GetAllCat()
        {
            var categoryies = _context.AppointmentCategoryMasters.Select(p => p.Name).ToList();
            return Ok(categoryies);
        }
    }
}
