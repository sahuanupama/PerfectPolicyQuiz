﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Models
{
    public class Question
    {
        public int QuestionId { get; set;}

        public string QuestionTopic { get; set; }

        public string QuestionText { get; set; }

        public string QuestionImage { get; set; }

        public string QuestionAsnwer { get; set; }

    }
}
