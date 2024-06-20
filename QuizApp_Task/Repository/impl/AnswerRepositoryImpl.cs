using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizApp_Task.Data;
using QuizApp_Task.Entities;

namespace QuizApp_Task.Repository.impl
{
    public class AnswerRepositoryImpl : BaseRepository, IAnswerRepository
    {
        private readonly QuizAppDbContext _dataDb;
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        public AnswerRepositoryImpl(QuizAppDbContext dataDB, IMapper mapper, IQuestionRepository questionRepository) : base(dataDB)
        {
            _dataDb = dataDB;
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        public AnswerEntity Add(AnswerEntity entity)
        {
            EntityEntry<AnswerEntity> entityEntry = _dataDb.tbl_answer.Add(entity);
            return entityEntry.Entity;
        }

        public bool Delete(AnswerEntity entity)
        {
            try
            {
                _dataDb.tbl_answer.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<AnswerEntity> getAll()
        {
            return _dataDb.tbl_answer.ToList();
        }

        public List<AnswerEntity> getAllByQuestionId(Guid id)
        {
            QuestionEntity? question = _questionRepository.GetById(id);
            if (question == null)
                throw new Exception($"Question not found with id: {id}");
            else
                return _dataDb.tbl_answer.Where(x => x.QuestionId == id).ToList();
        }

        public AnswerEntity? GetById(Guid id)
        {
            return _dataDb.tbl_answer.FirstOrDefault(a => a.Id == id);
        }

        public bool Update(AnswerEntity entity)
        {
            AnswerEntity? answer = GetById(entity.Id);
            if (answer == null)
                return false;
            else
                _dataDb.Entry(answer).State = EntityState.Detached;
            _dataDb.Attach(entity);
            _dataDb.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}
