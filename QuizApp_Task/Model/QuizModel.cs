using System.ComponentModel.DataAnnotations;

namespace QuizApp_Task.Model
{
    public class QuizModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
