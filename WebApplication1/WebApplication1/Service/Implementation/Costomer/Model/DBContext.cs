using Microsoft.EntityFrameworkCore;

namespace IncomeBridgeAPI.Service.Implementation.Costomer.Model
{
    public class DBContext : DbContext
    {
       
        public DBContext(DbContextOptions<DBContext> options)
         : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}
