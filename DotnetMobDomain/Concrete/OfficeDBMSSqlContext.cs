using Microsoft.EntityFrameworkCore;

namespace DotnetMobDomain.Concrete
{
    public class OfficeDBMSSqlContext : DbContext
    {
        public OfficeDBMSSqlContext(DbContextOptions options) : base(options)
        {
        }
        //Retrieve the Employees from the Database
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
    }
}
