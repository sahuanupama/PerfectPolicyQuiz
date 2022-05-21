using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class QuestionController : ControllerBase
    {
        private readonly PerfectPolicyQuizContext _context;

        public QuestionController(PerfectPolicyQuizContext context)
        {
            _context = context;
        }

        // GET: api/<QuestionController>
        // Get all Questions
        [HttpGet]
        public ActionResult<IEnumerable<Question>> GetQuestions(int quizid)
        {
            List<Question> Questions = new List<Question>();
            if (quizid > 0)
            {
                Questions = _context.Questions.Where(q => q.QuizId == quizid).ToList();
            }
            else
            {
                Questions = _context.Questions.ToList();
            }

            return Questions;
        }

        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        public ActionResult<Question> Get(int id)
        {
            Question question = _context.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }
            return question;
        }

        // POST api/<QuestionController>
        [HttpPost]
        public ActionResult<Question> Post(Question question)
        {
            if (question.QuizId < 1)
            {
                return BadRequest();
            }

            Question createdQuestion = new Question()
            {
                QuestionTopic = question.QuestionTopic,
                QuestionText = question.QuestionText,
                QuestionImage = question.QuestionImage,
                QuizId = question.QuizId
            };
            _context.Questions.Add(createdQuestion);

            _context.SaveChanges();
            // return CreatedAtAction("Post", createdQuestion);
            return Ok();
        }

        // PUT api/<QuestionController>/5
        //  [Authorize]
        [HttpPut("{id}")]
        public ActionResult<Question> Put(int id, [FromBody] Question question)
        {
            if (id != question.QuestionId)
            {
                return BadRequest();
            }

            _context.Questions.Update(question);
            _context.SaveChanges();
            return Ok(question);
        }

        // DELETE api/<QuestionController>/5
        // [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteQuestion(int id)
        {
            Question question = _context.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("QuestionsForQuizId/{id}")]
        public ActionResult QuestionsForQuizId(int id)
        {
            return Ok(_context.Questions.Where(c => c.QuizId == id));
        }

    }
}
