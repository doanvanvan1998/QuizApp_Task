using Microsoft.EntityFrameworkCore;
using QuizApp_Task.Entities;

namespace QuizApp_Task.Auth
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }
        public DbSet<DemoEntity> tbl_demo { get; set; }
        public DbSet<QuizEntity> tbl_quiz { get; set; }
        public DbSet<QuestionEntity> tbl_question { get; set; }
        public DbSet<AnswerEntity> tbl_answer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<QuizEntity>()
                .HasMany(qui => qui.QuestionEntities)
                .WithOne(que => que.QuizEntity)
                .HasForeignKey(que => que.QuizId)
                .IsRequired();

            builder.Entity<QuestionEntity>()
                .HasMany(que => que.AnswerEntities)
                .WithOne(ans => ans.QuestionEntity)
                .HasForeignKey(ans => ans.QuestionId)
                .IsRequired();

            base.OnModelCreating(builder);
        }
    }
}
