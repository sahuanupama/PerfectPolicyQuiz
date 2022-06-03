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

        /// <summary>
        /// Create report
        /// </summary>
        /// <param name="from">Start date for the report</param>
        /// <param name="to">End date for the report</param>
        /// <param name="name"> name of the quiz to generate report</param>
        /// <returns></returns>
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

            var QuizCountList = quiz.Select(c => new QuizCount
            {
                QuizPersonName = c.QuizTitle,
                QuestionCount = c.Questions.Count
            }).ToList();

            return Ok(QuizCountList);
        }
    }
}
