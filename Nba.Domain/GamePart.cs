using Adwaer.Entity;

namespace Nba.Domain
{
    public class GamePart : IEntity<int>
    {
        public int Id { get; set; }
        public int WinnerScore { get; set; }
        public int LoserScore { get; set; }
        public virtual Team Winner { get; set; }
        public virtual Team Loser { get; set; }
        public virtual Game Game { get; set; }
    }
}
