using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Models.Data
{
    public class QuestionListContext : DbContext
    {
        public QuestionListContext(DbContextOptions options ): base (options)
        {

        }

        public DbSet<Option> Options { get; set; }

         public DbSet<Question> Questions { get; set; }

        public DbSet<Quiz> Quizs { get; set; }

    }
}

