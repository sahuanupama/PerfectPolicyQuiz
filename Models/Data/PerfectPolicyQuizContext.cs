using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectPolicyQuiz.Models.Data
{
    public class PerfectPolicyQuizContext : DbContext
    {
        public PerfectPolicyQuizContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Option> Options { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Quiz> Quizs { get; set; }

        public DbSet<UserInfo> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Quiz>().HasData(
                new Quiz { QuizId = 1, QuizTitle = "Copyright", QuizDate = new DateTime(2020, 10, 15) }
                );
            builder.Entity<UserInfo>().HasData(
                new UserInfo { UserInfoId = 1, Username = "Anupama", Password = "1234_abc" });
        }

    }
}

