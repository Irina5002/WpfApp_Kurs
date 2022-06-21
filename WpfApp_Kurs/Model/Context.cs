using Microsoft.EntityFrameworkCore;

namespace WpfApp_Kurs.Model
{
    public class Context : DbContext
    {
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Reserve> Reserves { get; set; }

        public Context()
        {
            Database.EnsureCreated(); //Создает БД, если ее нет
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=KursRedBookApp;Trusted_Connection=True;");
        }
    }
}
