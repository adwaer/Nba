using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using Nba.Cqrs;
using Nba.Domain;

namespace Nba.Parser
{
    public class TeamsQuery : IQuery<Team[]>
    {
        private readonly DbContext _dbContext;

        //static TeamsQuery()
        //{
        //    AutoMapper.Mapper.Initialize(
        //        cfg => cfg.CreateMap<Profile, Team>()
        //                .ForMember(team => team.Division, opt => opt.MapFrom(profile => new Division { Name = profile.Division }))
        //                .ForMember(team => team.Conference, opt => opt.MapFrom(profile => new Conference { Name = profile.Conference }))
        //            );
        //}

        public TeamsQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Team[] Execute()
        {

            var teams = new List<Team>();
            var gameNodes = GetNodes("https://www.championat.com/basketball/_nba/2090/teams.html"
                , "//div[@class=\"sport__table\"]/table[1]/tr/td");

            foreach (var gameNode in gameNodes)
            {
                var singleNode = gameNode.SelectSingleNode("a");

                var url = singleNode.Attributes["href"].Value;
                var team = singleNode.SelectSingleNode("strong").InnerText;

                teams.Add(new Team
                {
                    Url = url,
                    Name = team
                });
            }

            return teams.ToArray();

            //var response = await new HttpClient()
            //    .GetStringAsync("http://ru.global.nba.com/stats2/league/conferenceteamlist.json?locale=ru");

            //var contract = JsonConvert.DeserializeObject<ConferenceTeamListResponse>(response);
            //if (contract.Error.IsError)
            //{
            //    throw new HttpRequestException($"http://ru.global.nba.com/stats2/league/conferenceteamlist.json?locale=ru query error: {contract.Error.Message}", new HttpRequestException(contract.Error.Detail));
            //}


            //var teams = contract
            //    .Payload
            //    .ListGroups
            //    .SelectMany(lg => lg.Teams
            //        .Select(t => t.Profile))
            //    .Select(AutoMapper.Mapper.Map<Team>)
            //    .ToArray();

            ////
            //// todo: this is query rules violation
            //var simpleQuery = new SimpleQuery(_dbContext);

            //var divisions = teams
            //    .Select(t => t.Division)
            //    .Distinct(new ByNameEqualityComparer<Division>())
            //    .ToDictionary(d => d.Name, d => simpleQuery.Execute(new DivisionByNameCondition(d.Name)).FirstOrDefault() ?? d);

            //var conferences = teams
            //    .Select(t => t.Conference)
            //    .Distinct(new ByNameEqualityComparer<Conference>())
            //    .ToDictionary(c => c.Name, c => simpleQuery.Execute(new ConferenceByNameCondition(c.Name)).FirstOrDefault() ?? c);

            //// ----
            ////

            //teams = teams
            //    .Select(t =>
            //    {
            //        t.Division = divisions[t.Division.Name];
            //        t.Conference = conferences[t.Conference.Name];

            //        return t;
            //    })
            //    .ToArray();


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
