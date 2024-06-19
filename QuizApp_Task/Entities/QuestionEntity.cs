using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.Entities
{
    public class QuestionEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(5000)]
        public string Content { get; set; }
        [Required]
        public string QuestionType { get; set; }

        public Guid QuizId { get; set; }
        public QuizEntity QuizEntity { get; set; } = null!;

        public ICollection<AnswerEntity> AnswerEntities { get; } = new List<AnswerEntity>();
    }
}
