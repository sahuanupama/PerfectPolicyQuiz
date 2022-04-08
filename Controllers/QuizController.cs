using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfectPolicyQuiz.Models;
using PerfectPolicyQuiz.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly PerfectPolicyQuizContext _context;

        public QuizController(PerfectPolicyQuizContext context)
        {
            _context = context;
        }

        // Get:api/Quiz

        [HttpGet]
        public ActionResult<IEnumerable<Quiz>> GetQuizs()
        {
            List<Quiz> Quizs = _context.Quizs.ToList();
            return Quizs;
        }
        // Get:api/<QuizController>/5
        [HttpGet("{id}")]

        public ActionResult<Quiz> GetQuiz(int id)
        {
            Quiz quiz = _context.Quizs.Find(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return quiz;
        }

        // Put:api/Quiz/5
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<Quiz> PutQuiz(int id, [FromBody] Quiz quiz)
        {
            if (id != quiz.QuizId)
            {
                return BadRequest();
            }

            _context.Quizs.Update(quiz);
            _context.SaveChanges();
            return Ok();

            /*_context.Entry(quiz).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return NoContent();
          */
        }

        // Post:api/Quiz
        [HttpPost]
        public ActionResult<Quiz> PostQuiz(Quiz quiz)
        {
            _context.Quizs.Add(quiz);
            _context.SaveChanges();

            //  return NoContent("PostQuiz");
            return NoContent();
        }

        // Delete Get:api/Quiz/5
        [HttpDelete("{id}")]
        public ActionResult DeleteQuiz(int id)
        {
            Quiz quiz = _context.Quizs.Find(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizs.Remove(quiz);
            _context.SaveChanges();

            return NotFound();
        }
    }
}
