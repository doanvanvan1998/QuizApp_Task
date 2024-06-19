using Microsoft.EntityFrameworkCore;
using QuizApp_Task.Entities;

namespace QuizApp_Task.Auth
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }
        public DbSet<DemoEntity> tbl_demo { get; set; }
    }
}
