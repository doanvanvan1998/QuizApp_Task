using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.DTO
{
    public class QuestionDto
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string QuestionType { get; set; }

    }
}
