using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.Entities
{
    public class UserQuizEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        [Required]
        public Guid QuizId { get; set; }
        public QuizEntity Quiz { get; set; } = null!;

        [Required]
        public string QuizCode { get; set; }

        [Required]
        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public ICollection<UserAnswerEntity> UserAnswers { get; } = new List<UserAnswerEntity>();
    }
}
