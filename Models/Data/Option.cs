using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Models.Data
{
    public class Option
    {
        public int OptionTextId {get; set;} 

        public string OptionLetters { get; set; }

        public string CorrectAnswer { get; set; }

    }
}
