using QuizApp_Task.DTO;
using QuizApp_Task.Model;

namespace QuizApp_Task.Service
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizDto>> GetAll();
        Task<QuizDto> GetById(Guid id);
        Task<QuizDto> Create(QuizModel model);
        Task<QuizDto> Update(Guid id, QuizModel model);
        Task<bool> Delete(Guid id);
    }
}
