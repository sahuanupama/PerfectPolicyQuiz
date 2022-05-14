using PerfectPolicyQuiz.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionToipc { get; set; }
        public string QuestionText { get; set; }
        public string QuestionImage { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
