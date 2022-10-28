using Microsoft.EntityFrameworkCore;

namespace MyWork
{
    public class AutoModel
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Mark { get; set; }
        public int Cost { get; set; }
        public int Amount { get; set; }
    }

    public class AutoModelContext : DbContext
    {
        public DbSet<AutoModel> Autos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Привязка контекста к существующей бд
            optionsBuilder.UseMySql("Server=MYSQL8001.site4now.net;Database=db_a8e04e_user090;Uid=a8e04e_user090;Pwd=qwerty5656;",
            new MySqlServerVersion(new Version(5,0)));
        }
    }
}
