using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.EndPoints
{
    [Authorize]
    [Route("api/answer-options")]
    [ApiController]
    public class AnswerOptionsEndpoint : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public AnswerOptionsEndpoint(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
     [Route("get-answeroption-byquestionid/{questionId}")]
        public ActionResult<IEnumerable<AnswerOption>> Get(long questionId)
        {
            return Ok(new { data = context.AnswerOptions.Where(q=>q.QuestionId==questionId).ToList() });

        }

        // GET api/<AnswerOptionsEndpoint>/5
        [HttpGet]
        [Route("get-all")]
        public ActionResult<AnswerOption> Get()
        {
            return Ok(new { data = context.AnswerOptions.Where(ansopts => ansopts.IsActive == true) });
        }
        [HttpGet("{id}")]
        public ActionResult<AnswerOption> Get(int id)
        {
            return Ok(context.AnswerOptions.Find(id));
        }

        // POST api/<AnswerOptionsEndpoint>
        [HttpPost]
        public ActionResult Post([FromForm] AnswerOption model)
        {
            try
            {
                model.CreatedBy = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.CreatedDate = DateTime.Now;
                context.AnswerOptions.Add(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<AnswerOptionsEndpoint>/5
        [HttpPut]
        public ActionResult Put([FromForm] AnswerOption model)
        {
            try
            {
                model.UpdatedBy = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.UpdatedDate = DateTime.Now;
                context.AnswerOptions.Update(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<AnswerOptionsEndpoint>/5
        [HttpDelete]
        public ActionResult Delete([FromForm] AnswerOption model)
        {
            try
            {
                context.AnswerOptions.Remove(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
