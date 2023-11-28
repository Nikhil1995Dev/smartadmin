using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Route("api/question")]
    [ApiController]
    public class QuestionEndPoint : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public QuestionEndPoint(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Question>> Get()
        {
            return   Ok(new { data = context.Questions.ToList() });
           
        }

        // GET api/<QuestionEndPoint>/5
        [HttpGet("{id}")]
        public ActionResult<Question> Get(int id)
        {
                return Ok(context.Questions.Find(id));
        }
        [HttpGet]
        [Route("get-question-by-sequence")]
        public ActionResult<Question> GetQuestionBySequence(int sequence)
        {
            var questions = context.Questions
                .Include(que => que.AnswerOptions)
                .Where(que => (que.IsActive ?? false) && (que.Sequence == sequence));
                //(from q in context.Questions
                //             join ans in context.An
                //             where q.IsActive == true
                //             select q);
            
            return Ok(questions);
        }

        // POST api/<QuestionEndPoint>
        [HttpPost]
        public ActionResult Post([FromForm] Question model)
        {
            try
            {
                model.CreatedBy =Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.CreatedDate = DateTime.Now;
                context.Questions.Add(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<QuestionEndPoint>/5
        [HttpPut]
        public ActionResult Put([FromForm] Question model)
        {
            try
            {
                model.UpdatedBy = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                model.UpdatedDate = DateTime.Now;
                context.Questions.Update(model);
                context.SaveChanges();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<QuestionEndPoint>/5
        [HttpDelete]
        public ActionResult Delete([FromForm] Question model)
        {
            try
            {
                context.Questions.Remove(model);
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
