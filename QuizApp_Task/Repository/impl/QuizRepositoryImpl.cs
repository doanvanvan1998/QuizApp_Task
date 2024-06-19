using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizApp_Task.Auth;
using QuizApp_Task.Entities;

namespace QuizApp_Task.Repository.impl
{
    public class QuizRepositoryImpl : BaseRepository, IQuizRepository
    {
        private readonly ApplicationDbContext _dataDb;
        private readonly IMapper _mapper;

        public QuizRepositoryImpl(ApplicationDbContext dataDB, IMapper mapper) : base(dataDB)
        {
            _dataDb = dataDB;
            _mapper = mapper;
        }

        public QuizEntity Add(QuizEntity entity)
        {
            EntityEntry<QuizEntity> entityEntry = _dataDb.tbl_quiz.Add(entity);
            return entityEntry.Entity;
        }

        public bool Delete(QuizEntity entity)
        {
            try
            {
                _dataDb.tbl_quiz.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<QuizEntity> getAll()
        {
            return _dataDb.tbl_quiz.ToList();
        }

        public QuizEntity? GetById(Guid id)
        {
            return _dataDb.tbl_quiz.FirstOrDefault(quiz => quiz.Id == id);
        }

        public bool Update(QuizEntity entity)
        {
            QuizEntity? ticket = GetById(entity.Id);
            if (ticket == null)
                return false;
            else
                _dataDb.Entry(ticket).State = EntityState.Detached;
            _dataDb.Attach(entity);
            _dataDb.Entry(entity).State = EntityState.Modified;
            return true;
        }
    }
}
