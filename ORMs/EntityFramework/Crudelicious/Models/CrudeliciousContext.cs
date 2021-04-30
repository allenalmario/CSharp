using Microsoft.EntityFrameworkCore;
namespace Crudelicious.Models
{
    // The context class is what forms the relationship between the models and the databases
    // This is how the ORM knows which tables in DB are connected to
    // Which Models / Classes
    public class CrudeliciousContext : DbContext // remember, this syntax is inheritance
    {
        public CrudeliciousContext(DbContextOptions options):base(options){}
        // these properties allow me to access the data stored in the database
        // Collection type from Entity Framework<data type> Db name (plural) get/set method
        public DbSet<Dish> Dishes {get;set;}
    }
}