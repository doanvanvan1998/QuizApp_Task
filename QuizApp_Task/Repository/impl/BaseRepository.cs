using QuizApp_Task.Data;

namespace QuizApp_Task.Repository.impl
{
    public class BaseRepository : IBaseRepository
    {
        private readonly QuizAppDbContext _dataDb;
        public BaseRepository(QuizAppDbContext dataDB)
        {
            _dataDb = dataDB;
        }

        public QuizAppDbContext DataDB { get; }

        public void SaveChanges()
        {
            _dataDb.SaveChanges();
        }
    }
}
