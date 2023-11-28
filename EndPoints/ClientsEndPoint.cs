using AutoMapper;
using BITS.SmartAdmin.WebUI.Extensions.UIExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Extensions;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartAdmin.WebUI.EndPoints
{
    [Authorize]
    [Route("api/client")]
    [ApiController]
    public class ClientsEndPoint : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public ClientsEndPoint(ApplicationDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ClientViewModel>> Get()
        {
            return Ok(new { data =mapper.Map<IEnumerable<ClientViewModel>>(context.Clients.ToList()) });

        }

        // GET api/<ClientsEndPoint>/5
        [HttpGet("{id}")]
        public ActionResult<Client> Get(int id)
        {
            return Ok(mapper.Map<ClientViewModel>(context.Clients.Find(id)));
        }

        // POST api/<ClientsEndPoint>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ClientViewModel clientViewModel)
        {
            try
            {
                var model = mapper.Map<Client>(clientViewModel);
                model.CreatedBy = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.CreatedDate = DateTime.Now;
                context.Clients.Add(model);
              int n=await  context.SaveChangesAsync();
                if(n>0)
                {
                    var lsttiminings = new List<Timing>();
                    lsttiminings.Add(new Timing { ClientId=model.Id,Day="Sunday",CreatedDate=DateTime.Now,IsActive=true});
                    lsttiminings.Add(new Timing { ClientId = model.Id, Day = "Monday", CreatedDate = DateTime.Now, IsActive = true });
                    lsttiminings.Add(new Timing { ClientId = model.Id, Day = "Tuesday", CreatedDate = DateTime.Now, IsActive = true });
                    lsttiminings.Add(new Timing { ClientId = model.Id, Day = "Wednesday", CreatedDate = DateTime.Now, IsActive = true });
                    lsttiminings.Add(new Timing { ClientId = model.Id, Day = "Thursday", CreatedDate = DateTime.Now, IsActive = true });
                    lsttiminings.Add(new Timing { ClientId = model.Id, Day = "Friday", CreatedDate = DateTime.Now, IsActive = true });
                    lsttiminings.Add(new Timing { ClientId = model.Id, Day = "Saturday", CreatedDate = DateTime.Now, IsActive = true });
                  await  context.AddRangeAsync(lsttiminings);
                    await context.SaveChangesAsync();

                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<ClientsEndPoint>/5
        [HttpPut]
        public ActionResult Put([FromForm] ClientViewModel clientViewModel)
        {
            try
            {
              var model =  mapper.Map<Client>(clientViewModel);
                model.UpdatedBy = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.UpdatedDate = DateTime.Now;
                context.Clients.Update(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<ClientsEndPoint>/5
        [HttpDelete]
        public ActionResult Delete([FromForm] ClientViewModel clientViewModel)
        {
            try
            {
                var model = mapper.Map<Client>(clientViewModel);
                context.Clients.Remove(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("lookuplist")]
        public ActionResult<IEnumerable<DropDown>> GetAllClient()
        {
            var clients = context.Clients.ToList();

            List<DropDown> commResponse = new List<DropDown>();
            commResponse.Add(new DropDown() { Id = Convert.ToString(0), Text = "miscellaneous" });
            foreach (var client in clients)
                commResponse.Add(new DropDown() { Id = client.Id.ToString(), Text = client.ClientName });

            return Ok(commResponse);
        }
    }
}
