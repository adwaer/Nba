using System.Data.Entity;
using Nba.Domain;

namespace Nba.Dal
{
    public class DefaultCtx : DbContext
    {
        public DefaultCtx() : base("ctx")
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<ScheduledGame> ScheduledGames { get; set; }
    }
}
