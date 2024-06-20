using System;
using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.Entities
{
    public class UserAnswerEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserQuizId { get; set; }
        public UserQuizEntity UserQuiz { get; set; } = null!;

        [Required]
        public Guid QuestionId { get; set; }
        public QuestionEntity Question { get; set; } = null!;

        [Required]
        public Guid AnswerId { get; set; }
        public AnswerEntity Answer { get; set; } = null!;

        [Required]
        public bool IsCorrect { get; set; }
    }
}
