using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using Nba.Cqrs;
using Nba.Cqrs.Query;
using Nba.Domain;
using Nba.Parser.QueryConditions;

namespace Nba.Parser
{
    public class GamesQuery : IQuery<Season, List<Game>>
    {
        private readonly DbContext _dbContext;
        private readonly Dictionary<string, Team> _teams;

        public GamesQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
            _teams = new Dictionary<string, Team>();
        }

        public List<Game> Execute(Season season)
        {
            List<Game> games = new List<Game>();

            var teamSeasonUrls = GetNodes(season.Url,
                "//div[@class=\"sport__table\"]/table[1]/tr/td[@class=\"_big\"]/a[@href]")
                .Select(a => a.Attributes["href"].Value);

            var simpleQuery = new SimpleQuery(_dbContext);

            foreach (var teamSeasonUrl in teamSeasonUrls)
            {
                var gamesNodes = GetNodes($"https://www.championat.com{teamSeasonUrl}",
                    "//div[@class=\"sport__table sport__table__tstat\"]/table[1]/tr");

                foreach (var gameNode in gamesNodes)
                {
                    var columns = gameNode.SelectNodes("td");
                    if (columns == null)
                    {
                        continue;
                    }

                    var strings = columns[4]
                        .InnerText
                        .Split(new[] { ' ', '\n', ':' }, StringSplitOptions.RemoveEmptyEntries);

                    if (strings[0] == "-")
                    {
                        break;
                    }

                    int[] score = null;
                    //try
                    //{
                    score = strings
                        .Take(2)
                    .Select(int.Parse)
                    .ToArray();
                    //}
                    //catch (Exception ex)
                    //{

                    //}

                    var gameUrl = columns[4]
                        .SelectSingleNode("a")
                        .Attributes["href"]
                        .Value;

                    if (games.Any(g => g.Url == gameUrl))
                    {
                        continue;
                    }

                    var date = columns[1].InnerText
                        .Split('.')
                        .Select(int.Parse)
                        .ToArray();

                    var time = columns[2].InnerText
                        .Split(':')
                        .Select(int.Parse)
                        .ToArray();

                    //var teams = columns[3].InnerText.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    var teamBreakerIndex = columns[3]
                        .InnerText
                        .IndexOf("&ndash;", StringComparison.Ordinal);

                    var firstTeamName = columns[3]
                        .InnerText
                        .Substring(0, teamBreakerIndex)
                        .Trim(' ', '\n');

                    var secondTeamName = columns[3]
                        .InnerText
                        .Substring(teamBreakerIndex + "&ndash;".Length)
                        .Trim(' ', '\n');

                    //int teamBreaker = 2;
                    //for (int i = 2; i < teams.Length; i++)
                    //{
                    //    var team = teams[i];
                    //    if (team == "&ndash;")
                    //    {
                    //        teamBreaker = i;
                    //        break;
                    //    }
                    //}

                    Team team1;
                    if (!_teams.TryGetValue(firstTeamName, out team1))
                    {
                        team1 = simpleQuery
                            .Execute(new TeamByNameCondition(firstTeamName))
                            .SingleOrDefault() ??
                                simpleQuery
                                    .Execute(new TeamByNameCondition(firstTeamName))
                                    .SingleOrDefault();
                        _teams[firstTeamName] = team1;
                    }
                    Team team2;
                    if (!_teams.TryGetValue(secondTeamName, out team2))
                    {
                        team2 = simpleQuery
                            .Execute(new TeamByNameCondition(secondTeamName))
                            .SingleOrDefault() ??
                                simpleQuery
                                    .Execute(new TeamByNameCondition(secondTeamName))
                                    .SingleOrDefault();
                        _teams[secondTeamName] = team2;
                    }

                    if (team1 == null || team2 == null)
                    {
                        continue;
                    }

                    var gamePartsNode = GetNodes($"https://www.championat.com{gameUrl}",
                        "//div[@class=\"match__count__extra\"]/div")
                        .First()
                        .InnerText
                        .Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);

                    while (gamePartsNode.Length < 4)
                    {
                        Thread.Sleep(10000);
                        gamePartsNode = GetNodes($"https://www.championat.com{gameUrl}",
                            "//div[@class=\"match__count__extra\"]/div")
                            .First()
                            .InnerText
                            .Split(new[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    var game = new Game
                    {
                        DateTime = new DateTime(date[2], date[1], date[0], time[0], time[1], 0),
                        WinnerScore = score[0] > score[1] ? score[0] : score[1],
                        LoserScore = score[0] > score[1] ? score[1] : score[0],
                        Winner = score[0] > score[1] ? team1 : team2,
                        Loser = score[0] > score[1] ? team2 : team1,
                        Season = season,
                        Url = gameUrl,
                        GameParts = new List<GamePart>()
                    };

                    foreach (var gamePart in gamePartsNode)
                    {
                        if (gamePart.Contains("ОТ"))
                        {
                            continue;
                        }

                        var partScore = gamePart
                            .Split(':')
                            .Select(int.Parse)
                            .ToArray();

                        game
                            .GameParts
                            .Add(new GamePart
                            {
                                Game = game,
                                WinnerScore = partScore[0] > partScore[1] ? partScore[0] : partScore[1],
                                Winner = partScore[0] > partScore[1] ? team1 : team2,
                                LoserScore = partScore[0] > partScore[1] ? partScore[1] : partScore[0],
                                Loser = partScore[0] > partScore[1] ? team2 : team1
                            });

                        games.Add(game);
                    }
                }
            }

            return games;
        }

        private static HtmlNodeCollection GetNodes(string url, string xpath)
        {
            int tryCount = 1;
            HtmlNodeCollection gamesNodes;
            l1:
            try
            {
                var request = WebRequest.CreateHttp(url);
                var response = request.GetResponse();

                using (var gameStream = response.GetResponseStream())
                {
                    var doc = new HtmlDocument();
                    doc.Load(gameStream, true);

                    gamesNodes =
                        doc.DocumentNode.SelectNodes(xpath);
                }
            }
            catch
            {
                if (tryCount < 10)
                {
                    tryCount++;
                    Thread.Sleep(1000);
                    goto l1;
                }
                throw;
            }
            return gamesNodes;
        }
    }
}
