using Microsoft.EntityFrameworkCore;

namespace DoctorsAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
    }
}
