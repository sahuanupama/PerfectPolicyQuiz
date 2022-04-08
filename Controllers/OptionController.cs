using Microsoft.AspNetCore.Mvc;
using PerfectPolicyQuiz.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public ActionResult<IEnumerable<Option>>GetOptions()
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
        public ActionResult <Option> PostOption(Option option)
        {
            _context.Options.Add(option);
            _context.SaveChanges();
            return NoContent();
        }

        // PUT api/<OptionController>/5
        [HttpPut("{id}")]
        public ActionResult PutOption(int id,Option option)
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
