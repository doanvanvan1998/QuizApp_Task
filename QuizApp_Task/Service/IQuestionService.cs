using QuizApp_Task.DTO;
using QuizApp_Task.Entities;
using QuizApp_Task.Model;

namespace QuizApp_Task.Service
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetAll();
        Task<QuestionDto> GetById(Guid id);
        Task<QuestionDto> Create(QuestionModel model);
        Task<QuestionDto> Update(Guid id, QuestionModel model);
        Task<bool> Delete(Guid id);
        IEnumerable<QuestionDto> getAllByQuizId(Guid id);
    }
}
