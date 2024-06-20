using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizApp_Task.Entities;
using System;
using System.Reflection.Emit;

namespace QuizApp_Task.Data
{
    public class QuizAppDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid,
        IdentityUserClaim<Guid>, UserRoleEntity, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public QuizAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<QuizEntity> tbl_quiz { get; set; }
        public DbSet<QuestionEntity> tbl_question { get; set; }
        public DbSet<AnswerEntity> tbl_answer { get; set; }
        public DbSet<UserQuizEntity> tbl_userQuizzes { get; set; }
        public DbSet<UserAnswerEntity> tbl_userAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<QuizEntity>()
                .HasMany(q => q.QuestionEntities)
                .WithOne(qe => qe.QuizEntity)
                .HasForeignKey(qe => qe.QuizId)
                .IsRequired();

            builder.Entity<QuestionEntity>()
                .HasMany(qe => qe.AnswerEntities)
                .WithOne(ae => ae.QuestionEntity)
                .HasForeignKey(ae => ae.QuestionId)
                .IsRequired();

            builder.Entity<UserEntity>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                entity.Ignore(e => e.DisplayName);
                entity.Property(e => e.DateOfBirth).IsRequired();
                entity.Property(e => e.Avatar).HasMaxLength(500);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            builder.Entity<RoleEntity>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(50).IsRequired();
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            builder.Entity<UserRoleEntity>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(e => e.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(e => e.RoleId);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(e => e.UserId);
            });

            builder.Entity<UserQuizEntity>()
            .HasOne(uq => uq.User)
            .WithMany(u => u.UserQuizzes)
            .HasForeignKey(uq => uq.UserId);

            builder.Entity<UserQuizEntity>()
                .HasOne(uq => uq.Quiz)
                .WithMany(q => q.UserQuizzes)
                .HasForeignKey(uq => uq.QuizId);

            builder.Entity<UserAnswerEntity>()
                .HasOne(ua => ua.UserQuiz)
                .WithMany(uq => uq.UserAnswers)
                .HasForeignKey(ua => ua.UserQuizId);

            builder.Entity<UserAnswerEntity>()
                .HasOne(ua => ua.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(ua => ua.QuestionId);

            builder.Entity<UserAnswerEntity>()
                .HasOne(ua => ua.Answer)
                .WithMany(a => a.UserAnswers)
                .HasForeignKey(ua => ua.AnswerId);
        }
    }
}
