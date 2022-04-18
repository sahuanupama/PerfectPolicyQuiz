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
        /*[HttpGet]
        public ActionResult<IEnumerable<Quiz>> GetQuizs()
        {
            List<Quiz> Quizs = _context.Quizs.ToList();
            return Quizs;
        }*/

        // Get:api/Quiz
        [HttpGet]
        public ActionResult<IEnumerable<Quiz>> GetQuizsForTopic(String topic)
        {
            List<Quiz> Quizs = new List<Quiz>();
            if (topic != null)
            {
                List<int> quizIds = _context.Questions.Where(q => q.QuestionTopic == topic).Select(q => q.QuizId).Distinct().ToList();

                foreach (int qId in quizIds)
                {
                    Quiz quiz = _context.Quizs.Find(qId);
                    Quizs.Add(quiz);
                }
            }
            else
            {
                Quizs = _context.Quizs.ToList();
            }

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

        // Post:api/Quiz
        [HttpPost]
        public ActionResult<Quiz> PostQuiz(Quiz quiz)
        {
            if (quiz == null)
            {
                return BadRequest();
            }

            Quiz newQuiz = new Quiz()
            {
                QuizTitle = quiz.QuizTitle,
                QuizDate = quiz.QuizDate,
                QuizPersonName = quiz.QuizPersonName,
                QuizPassNumber = quiz.QuizPassNumber
            };
            _context.Quizs.Add(newQuiz);
            _context.SaveChanges();
            return CreatedAtAction("PostQuiz", newQuiz);
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
            return Ok(quiz);
        }

        // Delete Get:api/Quiz/5
        [Authorize]
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

            return Ok();
        }
    }
}
