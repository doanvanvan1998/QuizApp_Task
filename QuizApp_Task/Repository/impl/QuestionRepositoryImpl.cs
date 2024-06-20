using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizApp_Task.Data;
using QuizApp_Task.Entities;
using QuizApp_Task.Service;

namespace QuizApp_Task.Repository.impl
{
    public class QuestionRepositoryImpl : BaseRepository, IQuestionRepository
    {
        private readonly QuizAppDbContext _dataDb;
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;

        public QuestionRepositoryImpl(QuizAppDbContext dataDb, IMapper mapper, IQuizRepository quizRepository) : base(dataDb)
        {
            _dataDb = dataDb;
            _mapper = mapper;
            _quizRepository = quizRepository;
        }

        public QuestionEntity Add(QuestionEntity entity)
        {
            EntityEntry<QuestionEntity> entityEntry = _dataDb.tbl_question.Add(entity);
            return entityEntry.Entity;
        }

        public bool Delete(QuestionEntity entity)
        {
            try
            {
                _dataDb.tbl_question.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<QuestionEntity> getAll()
        {
            return _dataDb.tbl_question.ToList();
        }

        public List<QuestionEntity> getAllByQuizId(Guid id)
        {
            QuizEntity? quiz = _quizRepository.GetById(id);
            if (quiz == null)
                throw new Exception($"Quiz not found with id: {id}");
            else
                return _dataDb.tbl_question.Include(q => q.AnswerEntities).Where(x => x.QuizId == id).ToList();
        }

        public QuestionEntity? GetById(Guid id)
        {
            return _dataDb.tbl_question.Include(q => q.AnswerEntities).FirstOrDefault(question => question.Id == id);
        }

        public bool Update(QuestionEntity entity)
        {
            QuestionEntity? question = GetById(entity.Id);
            if (question == null)
                return false;
            else
                _dataDb.Entry(question).State = EntityState.Detached;
            _dataDb.Attach(entity);
            _dataDb.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}
