using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Extensions;
using SmartAdmin.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.EndPoints
{
    [Route("api/broadcast")]
    public class BroadCastEndPoint : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;

        public BroadCastEndPoint(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("get-by-clientId/{ClientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BroadCastViewModel>> GetByClientId([FromRoute] long ClientId)
        {
            List<BroadCastViewModel> broadCasts =_mapper.Map<List<BroadCastViewModel>>(_context.BroadCasts.Where(c=>c.ClientId==ClientId).ToList());
            return Ok(new { data = broadCasts, recordsTotal = broadCasts.Count, recordsFiltered = broadCasts.Count });
        }
        

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BroadCastViewModel> Get([FromRoute] long id)
        {
            BroadCastViewModel broadCast =_mapper.Map<BroadCastViewModel>(_context.BroadCasts.Find(id));
            return Ok(broadCast);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
    
        public IActionResult Create([FromForm] BroadCastViewModel model)
        {

            model.CreatedDate = DateTime.Now;
            model.CreatedBy = User.GetUserId();

           _context.BroadCasts.Add(_mapper.Map<BroadCast>(model));

            if (_context.SaveChanges()>0)
            {
                return Ok(new { ResponseCode = "success", ResponseMessage = "BroadCast created successful!!!!!!!" });
            }
            else
            {
                
                return Ok(new  { ResponseCode = "error", ResponseMessage = "Something went wrong." });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
   
        public IActionResult Update([FromForm] BroadCastViewModel model)
        {
          
            model.UpdatedDate = DateTime.Now;
            model.UpdatedBy = User.GetUserId();
            _context.BroadCasts.Update(_mapper.Map<BroadCast>(model));

            if (_context.SaveChanges()>0)
            {
                return Ok(new  { ResponseCode = "success", ResponseMessage = "BroadCast updated successfully" });
            }
            else
            {
                
                return Ok(new  { ResponseCode = "error", ResponseMessage = "Something went wrong."  });
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
       
        public IActionResult Delete([FromForm] BroadCastViewModel model)
        {
            _context.Remove(_mapper.Map<BroadCast>(model));
            if (_context.SaveChanges()>0)
            {
                return Ok(new{ ResponseCode = "success", ResponseMessage = "BroadCast deleted successfully" });
            }

            else
            {
               
                return Ok(new { ResponseCode = "error", ResponseMessage = "Something went wrong." });
            }
        }

    }
}
