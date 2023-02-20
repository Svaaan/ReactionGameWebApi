using Microsoft.EntityFrameworkCore;
namespace webapi.Model;

public class MyDbContext : DbContext
{

    public MyDbContext(DbContextOptions<MyDbContext> options)
      : base(options)
    {
    }
    public DbSet<Highscore> highscores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source ={"ReactionGame.db"}");
    }
}