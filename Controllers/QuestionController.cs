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
            if (question == null)
            {
                return BadRequest();
            }

            /* Quiz newQuiz = new Quiz()
             {
                 QuizTitle = question.Quiz.QuizTitle,
                 QuizDate = question.Quiz.QuizDate,
                 QuizPersonName = question.Quiz.QuizPersonName,
                 QuizPassNumber = question.Quiz.QuizPassNumber
             };
             _context.Quizs.Add(newQuiz);*/

            if (question.Quiz != null)
            {
                foreach (Question newQuestion in question.Quiz.Questions)
                {
                    Question multiquestions = new Question()
                    {
                        QuestionTopic = newQuestion.QuestionTopic,
                        QuestionText = newQuestion.QuestionText,
                        QuestionImage = newQuestion.QuestionImage,
                        QuizId = question.Quiz.QuizId
                    };
                    _context.Questions.Add(multiquestions);
                }
            }
            else
            {

                Question createdQuestion = new Question()
                {
                    // QuestionId = question.QuestionId,
                    QuestionTopic = question.QuestionTopic,
                    QuestionText = question.QuestionText,
                    QuestionImage = question.QuestionImage,
                    QuizId = question.QuizId
                };
                _context.Questions.Add(createdQuestion);
            }
            /* foreach (Option newOption in question.Options)
             {
                 Option option = new Option()
                 {
                     // OptionId = newOption.OptionId,
                     OptionText = newOption.OptionText,
                     OptionNumber = newOption.OptionNumber,
                     QuestionId = newOption.QuestionId
                 };
                 _context.Options.Add(option);
             }*/
            _context.SaveChanges();
            // return CreatedAtAction("Post", createdQuestion);
            return Ok();
        }

        // PUT api/<QuestionController>/5
        [Authorize]
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
        [Authorize]
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
    }
}
