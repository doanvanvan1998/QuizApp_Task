using QuizApp_Task.Entities;

namespace QuizApp_Task.Repository
{
    public interface IAnswerRepository : IBaseRepository
    {
        List<AnswerEntity> getAll();
        AnswerEntity? GetById(Guid id);
        AnswerEntity Add(AnswerEntity entity);
        bool Update(AnswerEntity entity);
        bool Delete(AnswerEntity entity);
        List<AnswerEntity> getAllByQuestionId(Guid id);
    }
}
