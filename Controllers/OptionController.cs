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
        public ActionResult<IEnumerable<Option>> GetOptions()
        {
            List<Option> Options = _context.Options.ToList();
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
            /*if (option.Question.Options != null)
            {
                foreach (Option newOption in option.Question.Options)
                {
                    Option op = new Option()
                    {
                        OptionText = newOption.OptionText,
                        OptionNumber = newOption.OptionNumber,
                        QuestionId = newOption.QuestionId
                    };
                    _context.Options.Add(op);
                }
            }*/

            Option createdOption = new Option()
            {
                OptionText = option.OptionText,
                OptionNumber = option.OptionNumber,
                QuestionId = option.QuestionId
            };

            _context.Options.Add(createdOption);
            _context.SaveChanges();
            return CreatedAtAction("PostOption", createdOption);
        }

        // PUT api/<OptionController>/5
        [HttpPut("{id}")]
        public ActionResult PutOption(int id, Option option)
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
            return NoContent();
        }

        // DELETE api/<OptionController>/5
        [HttpDelete("{id}")]
        public NotFoundResult Delete(int id)
        {
            Option option = _context.Options.Find(id);
            if (option == null)
            {
                return NotFound();
            }
            _context.Options.Remove(option);
            _context.SaveChanges();
            return NotFound();
        }
    }
}
