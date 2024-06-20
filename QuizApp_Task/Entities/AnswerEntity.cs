using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.Entities
{
    public class AnswerEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Content { get; set; }

        [Required]
        public bool IsCorrect { get; set; } = false;

        public Guid QuestionId { get; set; }
        public QuestionEntity QuestionEntity { get; set; } = null!;
        public ICollection<UserAnswerEntity> UserAnswers { get; } = new List<UserAnswerEntity>();
    }
}
