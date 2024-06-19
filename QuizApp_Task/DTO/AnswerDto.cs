namespace QuizApp_Task.DTO
{
    public class AnswerDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; } = false;
    }
}
