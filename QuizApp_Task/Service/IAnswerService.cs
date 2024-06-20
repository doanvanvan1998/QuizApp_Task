using QuizApp_Task.DTO;
using QuizApp_Task.Model;

namespace QuizApp_Task.Service
{
    public interface IAnswerService
    {
        Task<IEnumerable<AnswerDto>> GetAll();
        Task<AnswerDto> GetById(Guid id);
        Task<AnswerDto> Create(AnswerModel model);
        Task<AnswerDto> Update(Guid id, AnswerModel model);
        Task<bool> Delete(Guid id);
        IEnumerable<AnswerDto> getAllByQuestionId(Guid id);
    }
}
