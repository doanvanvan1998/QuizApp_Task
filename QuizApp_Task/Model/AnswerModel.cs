using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.Model
{
    public class AnswerModel
    {
        public string Content { get; set; }

        public bool IsCorrect { get; set; } = false;
        public Guid QuestionId { get; set; }
    }
}
