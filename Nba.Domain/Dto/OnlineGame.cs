namespace Nba.Domain.Dto
{
    public class OnlineGame
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public OnlineGameStat[] Totals { get; set; }
        public OnlineGameStat[] Handicaps { get; set; }
    }
}
