using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Nba.Cqrs;
using Nba.Cqrs.Query;
using Nba.Domain;
using Nba.Parser.QueryConditions;

namespace Nba.Parser
{
    public class ScheduledGamesQuery : IQuery<ScheduledGame[]>
    {
        private readonly DbContext _dbContext;

        public ScheduledGamesQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ScheduledGame[] Execute()
        {
            List<ScheduledGame> games = new List<ScheduledGame>();
            var gameNodes = GetNodes("https://www.championat.com/basketball/_nba/2090/calendar.html"
                , "//div[@class=\"sport__table sport__calendar__table\"]/table[1]/tbody/tr");

            var simpleQuery = new SimpleQuery(_dbContext);

            foreach (var gameNode in gameNodes)
            {
                var columns = gameNode.SelectNodes("td");

                var url = columns[0].SelectSingleNode("a").Attributes["href"].Value;
                var date = columns[1]
                    .InnerText
                    .Replace("\n", string.Empty)
                    .Replace(" ", string.Empty)
                    .Split(new[] { ',', '.', ':' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var teamsName = columns[2].SelectNodes("a")
                    .Select(a => a.InnerText)
                    .ToArray();

                var team1 = simpleQuery
                        .Execute(new TeamByCityCondition(teamsName[0]))
                        .SingleOrDefault() ??
                        simpleQuery
                        .Execute(new TeamByNameCondition(teamsName[0]))
                        .SingleOrDefault();

                var team2 = simpleQuery
                    .Execute(new TeamByCityCondition(teamsName[1]))
                    .SingleOrDefault() ??
                        simpleQuery
                        .Execute(new TeamByNameCondition(teamsName[1]))
                        .SingleOrDefault();

                games.Add(new ScheduledGame
                {
                    Url = $"https://www.championat.com/{url}",
                    Date = new DateTime(date[2], date[1], date[0], date[3], date[4], 0),
                    Team1 = team1,
                    Team2 = team2
                });
            }

            return games.ToArray();
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
