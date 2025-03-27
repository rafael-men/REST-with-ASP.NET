using main.Models;
using Microsoft.EntityFrameworkCore;

namespace main.Data
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext() { }
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

    }
}
