using QuizApp_Task.Entities;

namespace QuizApp_Task.Repository
{
    public interface IQuizRepository : IBaseRepository
    {
        List<QuizEntity> getAll();
        QuizEntity? GetById(Guid id);
        QuizEntity Add(QuizEntity entity);
        bool Update(QuizEntity entity);
        bool Delete(QuizEntity entity);
    }
}
