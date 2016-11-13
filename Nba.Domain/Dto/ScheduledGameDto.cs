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
        public QuarterGameDto[] Stat { get; set; }
        public GameStatDto[] Games { get; set; }
        //public GameDto[] HistoryGames { get; set; }

        public static ScheduledGameDto Get(ScheduledGame game, Game[] games)
        {
            //var dictionary = new Dictionary<int, List<GamePart>>();
            var stat = new[]
            {
                new QuarterGameDto
                {
                    Parts = new List<QuarterGamePartDto>(),
                    Percent = 0
                },
                new QuarterGameDto
                {
                    Parts = new List<QuarterGamePartDto>(),
                    Percent = 0
                },
                new QuarterGameDto
                {
                    Parts = new List<QuarterGamePartDto>(),
                    Percent = 0
                },
                new QuarterGameDto
                {
                    Parts = new List<QuarterGamePartDto>(),
                    Percent = 0
                }
            };

            var gameDtos = new List<GameStatDto>();

            foreach (var g in games)
            {
                gameDtos.Add(new GameStatDto
                {
                    Url = g.Url,
                    Date = g.DateTime,
                    Team1Win = g.Winner == game.Team1,
                    Team1Score = g.Winner == game.Team1 ? g.WinnerScore : g.LoserScore,
                    Team2Score = g.Winner == game.Team1 ? g.LoserScore : g.WinnerScore
                });

                var gameParts = g.GameParts.ToArray();
                for (int i = 0; i < 4; i++)
                {
                    var gamePart = gameParts[i];
                    var isTeam1Winner = gamePart.Winner == game.Team1;

                    var quarterGameDto = stat[i];
                    if (isTeam1Winner)
                    {
                        quarterGameDto.Percent += 100;
                        quarterGameDto.Parts.Add(new QuarterGamePartDto
                        {
                            Team1Score = gamePart.WinnerScore,
                            Team1Win = true,
                            Team2Score = gamePart.LoserScore,
                            Date = game.Date
                        });
                    }
                    else
                    {
                        quarterGameDto.Parts.Add(new QuarterGamePartDto
                        {
                            Team1Score = gamePart.LoserScore,
                            Team1Win = false,
                            Team2Score = gamePart.WinnerScore,
                            Date = game.Date
                        });
                    }
                    
                }
            }

            for (int i = 0; i < 4; i++)
            {
                stat[i].Percent = stat[i].Percent / games.Length;
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
                Stat = stat,
                Games = gameDtos.ToArray()
                //HistoryGames = games.Select(GameDto.Get).ToArray()
            };
        }
    }
}
