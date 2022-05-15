using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfectPolicyQuiz.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly PerfectPolicyQuizContext _context;


        public OptionController(PerfectPolicyQuizContext context)
        {
            _context = context;
        }

        // GET: api/<OptionController>
        [HttpGet]
        public ActionResult<IEnumerable<Option>> GetOptions(int questionId)
        {
            List<Option> Options = new List<Option>();

            if (questionId > 0)
            {
                Options = _context.Options.Where(o => o.QuestionId == questionId).ToList();
            }
            else
            {
                Options = _context.Options.ToList();
            }
            return Options;
        }

        // GET api/<OptionController>/5
        [HttpGet("{id}")]
        public ActionResult<Option> GetOption(int id)
        {
            Option option = _context.Options.Find(id);

            if (option == null)
            {
                return NotFound();
            }
            return option;
        }

        // POST api/<OptionController>
        [HttpPost]
        public ActionResult<Option> PostOption(Option option)
        {
            if (option == null)
            {
                return BadRequest();
            }

            Option createdOption = new Option()
            {
                OptionText = option.OptionText,
                OptionNumber = option.OptionNumber,
                QuestionId = option.QuestionId
            };
            _context.Options.Add(createdOption);
            _context.SaveChanges();
            return Ok();
        }

        // PUT api/<OptionController>/5
        // [Authorize]
        [HttpPut("{id}")]
        public ActionResult<Option> PutOption(int id, Option option)
        {
            _context.Entry(option).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return Ok(option);
        }

        // DELETE api/<OptionController>/5
        // [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Option option = _context.Options.Find(id);
            if (option == null)
            {
                return NotFound();
            }
            _context.Options.Remove(option);
            _context.SaveChanges();
            return Ok();
        }
    }
}
