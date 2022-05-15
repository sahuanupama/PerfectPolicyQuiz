using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Models.Data
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public DateTime QuizDate { get; set; }
        public string QuizPersonName { get; set; }
        public int QuizPassNumber { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
