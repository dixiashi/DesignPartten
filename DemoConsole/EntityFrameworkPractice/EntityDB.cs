using System.Data.Entity;

namespace DemoConsole.EntityFrameworkPractice
{
    public class EntityDB : DbContext
    {
        public EntityDB() : base("ConnectionString")
        {

        }

        public virtual DbSet<Experience> Experience { get; set; }

    }
}
