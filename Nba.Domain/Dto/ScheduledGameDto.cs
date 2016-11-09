using System;
using System.Collections.Generic;
using System.Linq;

namespace Nba.Domain.Dto
{
    public class ScheduledGameDto
    {
        public DateTime Date { get; set; }
        public TeamDto Team1 { get; set; }
        public TeamDto Team2 { get; set; }
        public decimal Team1WinPercent { get; set; }
        public Dictionary<int, decimal> Stat { get; set; }
        //public GameDto[] HistoryGames { get; set; }

        public static ScheduledGameDto Get(ScheduledGame game, Game[] games)
        {
            //var dictionary = new Dictionary<int, List<GamePart>>();
            var stat = new Dictionary<int, decimal> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 } };

            foreach (var g in games)
            {
                var gameParts = g.GameParts.ToArray();
                for (int i = 0; i < 4; i++)
                {
                    var gamePart = gameParts[i];
                    var isTeam1Winner = gamePart.Winner == game.Team1;
                    if (isTeam1Winner)
                    {
                        stat[i + 1] += 1;
                    }

                    //dictionary[i] = dictionary[i] ?? new List<GamePart>();
                    //dictionary[i].Add(gamePart);
                }
            }

            for (int i = 1; i <= 4; i++)
            {
                stat[i] = stat[i] / games.Length;
            }
            
            var team1WinPercent = games.Sum(g => g.Winner == game.Team1 ? 100 : 0) / games.Length;

            //var stat = new Dictionary<int, decimal>();
            //for (int i = 0; i < dictionary.Count; i++)
            //{
            //    var gameParts = dictionary[i];

            //    foreach (var gamePart in gameParts)
            //    {

            //    }

            //}

            return new ScheduledGameDto
            {
                Date = game.Date,
                Team1 = TeamDto.Get(game.Team1),
                Team2 = TeamDto.Get(game.Team2),
                Team1WinPercent = team1WinPercent,
                Stat = stat
                //HistoryGames = games.Select(GameDto.Get).ToArray()
            };
        }
    }
}
