using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartAdmin.WebUI.EndPoints
{
    [Authorize]
    [Route("api/setting")]
    [ApiController]
    public class SettingEndPoint : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public SettingEndPoint(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Setting>> Get()
        {
            return Ok(new { data = context.Settings.ToList() });

        }

        // GET api/<SettingEndPoint>/5
        [HttpGet("{id}")]
        public ActionResult<Setting> Get(int id)
        {
            return Ok(context.Settings.Find(id));
        }

        // POST api/<SettingEndPoint>
        [HttpPost]
        public ActionResult Post([FromForm] Setting model)
        {
            try
            {
                model.CreatedBy = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.CreatedDate = DateTime.Now;
                context.Settings.Add(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<SettingEndPoint>/5
        [HttpPut]
        public ActionResult Put([FromForm] Setting model)
        {
            try
            {
                model.UpdatedBy = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.UpdatedDate = DateTime.Now;
                context.Settings.Update(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<SettingEndPoint>/5
        [HttpDelete]
        public ActionResult Delete([FromForm] Setting model)
        {
            try
            {
                context.Settings.Remove(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
