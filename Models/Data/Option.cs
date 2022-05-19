using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Models.Data
{
    public class Option
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }
        public string OptionNumber { get; set; }
        public int QuestionId { get; set; }
    }
}
