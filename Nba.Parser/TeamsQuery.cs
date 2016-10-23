using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Nba.Cqrs;
using Nba.Cqrs.Query;
using Nba.Dal;
using Nba.Domain;
using Nba.Domain.Comparers;
using Nba.Parser.Contract;
using Nba.Parser.QueryConditions;
using Newtonsoft.Json;

namespace Nba.Parser
{
    public class TeamsQuery : IQuery<Task<Team[]>>
    {
        private readonly DbContext _dbContext;

        static TeamsQuery()
        {
            AutoMapper.Mapper.Initialize(
                cfg => cfg.CreateMap<Profile, Team>()
                        .ForMember(team => team.Division, opt => opt.MapFrom(profile => new Division { Name = profile.Division }))
                        .ForMember(team => team.Conference, opt => opt.MapFrom(profile => new Conference { Name = profile.Conference }))
                    );
        }

        public TeamsQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Team[]> Execute()
        {
            var response = await new HttpClient()
                .GetStringAsync("http://ru.global.nba.com/stats2/league/conferenceteamlist.json?locale=ru");

            var contract = JsonConvert.DeserializeObject<ConferenceTeamListResponse>(response);
            if (contract.Error.IsError)
            {
                throw new HttpRequestException($"http://ru.global.nba.com/stats2/league/conferenceteamlist.json?locale=ru query error: {contract.Error.Message}", new HttpRequestException(contract.Error.Detail));
            }


            var teams = contract
                .Payload
                .ListGroups
                .SelectMany(lg => lg.Teams
                    .Select(t => t.Profile))
                .Select(AutoMapper.Mapper.Map<Team>)
                .ToArray();

            //
            // todo: this is query rules violation
            var simpleQuery = new SimpleQuery(_dbContext);

            var divisions = teams
                .Select(t => t.Division)
                .Distinct(new ByNameEqualityComparer<Division>())
                .ToDictionary(d => d.Name, d => simpleQuery.Execute(new DivisionByNameCondition(d.Name)).FirstOrDefault() ?? d);

            var conferences = teams
                .Select(t => t.Conference)
                .Distinct(new ByNameEqualityComparer<Conference>())
                .ToDictionary(c => c.Name, c => simpleQuery.Execute(new ConferenceByNameCondition(c.Name)).FirstOrDefault() ?? c);

            // ----
            //

            teams = teams
                .Select(t =>
                {
                    t.Division = divisions[t.Division.Name];
                    t.Conference = conferences[t.Conference.Name];

                    return t;
                })
                .ToArray();


            return teams;
        }
    }
}
