using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Nba.Cqrs;
using Nba.Dal;
using Nba.Domain.Dto;

namespace Nba.Queries
{
    public class SeasonsQuery : IQuery<Task<SeasonDto[]>>
    {
        private static SeasonDto[] Cache { get; set; }
        public async Task<SeasonDto[]> Execute()
        {
            if (Cache == null)
            {
                var ctx = new DefaultCtx();
                var seasons = (await ctx.Seasons
                    .ToArrayAsync())
                    .Select(s => new SeasonDto
                    {
                        Url = s.Url,
                        Name = $"{s.StartDate.Year} - {s.EndDate.Year}",
                        Id = s.Id
                    });

                Cache = seasons.ToArray();
            }

            return Cache;
        }
    }
}
