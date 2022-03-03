using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Models.Data
{
    public class QuestionList
    {
        public int OptionTextId { get; set; }

        public int QuestionId { get; set; }

        public int  QuizId{ get; set; }
    }
}
