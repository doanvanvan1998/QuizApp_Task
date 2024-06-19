using QuizApp_Task.Auth;

namespace QuizApp_Task.Repository.impl
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext _dataDb;
        public BaseRepository(ApplicationDbContext dataDB)
        {
            _dataDb = dataDB;
        }

        public ApplicationDbContext DataDB { get; }

        public void SaveChanges()
        {
            _dataDb.SaveChanges();
        }
    }
}
