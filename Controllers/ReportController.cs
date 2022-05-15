using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectPolicyQuiz.Models.Data;
using PerfectPolicyQuiz.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly PerfectPolicyQuizContext _context;

        public ReportController(PerfectPolicyQuizContext context)
        {
            _context = context;
        }

        [HttpGet("QuizCountReport")]
        public IActionResult QuizCountReport(DateTime? from, DateTime? to, string? name)
        {
            var quiz = _context.Quizzes.Include(c => c.Questions);
            if (from != null)
            {
                quiz.Where(c => c.QuizDate > from);
            }
            if (to != null)
            {
                quiz.Where(c => c.QuizDate > to);
            };


            var QuizCountlist = quiz.Select(c => new QuizCount
            {
                QuizPersonName = c.QuizPersonName,
                QuestionCount = c.Questions.Count
            }).ToList();

            return Ok(QuizCountlist);
        }
    }
}
