using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.Entities
{
    public class QuizEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [MinLength(5)]
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(1, 3600)]
        [Required]
        public int Duration { get; set; }

        [MaxLength(500)]
        public string ThumbnailUrl { get; set; }

        public ICollection<QuestionEntity> QuestionEntities { get; } = new List<QuestionEntity>();
    }
}
