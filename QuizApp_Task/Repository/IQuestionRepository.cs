using QuizApp_Task.Entities;

namespace QuizApp_Task.Repository
{
    public interface IQuestionRepository : IBaseRepository
    {
        List<QuestionEntity> getAll();
        QuestionEntity? GetById(Guid id);
        QuestionEntity Add(QuestionEntity entity);
        bool Update(QuestionEntity entity);
        bool Delete(QuestionEntity entity);
        List<QuestionEntity> getAllByQuizId(Guid id);
    }
}
