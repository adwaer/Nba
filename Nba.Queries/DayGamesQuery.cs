using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Nba.Cqrs;
using Nba.Dal;
using Nba.Domain.Dto;
using Nba.Queries.Criteria;

namespace Nba.Queries
{
    public class DayGamesQuery : IQuery<DayGamesCondition, Task<ScheduledGameDto[]>>
    {
        public async Task<ScheduledGameDto[]> Execute(DayGamesCondition condition)
        {
            var date = condition.Date.Value;

            var tomorrow = date.Date.AddDays(1);

            var ctx = new DefaultCtx();
            var scheduledGames = await ctx
                .ScheduledGames
                .Include(sg => sg.Team1)
                .Include(sg => sg.Team2)
                .Where(sg => sg.Date >= date.Date && sg.Date < tomorrow)
                .ToArrayAsync();

            var gamesQueryable = ctx.Games
                .Include("GameParts.Loser")
                .Include("GameParts.Winner")
                .Include(g => g.Loser)
                .Include(g => g.Winner);

            if (condition.SeasonId.HasValue)
            {
                gamesQueryable = gamesQueryable
                    .Where(g => g.Season.Id >= condition.SeasonId.Value);
            }

            var teamGames = new List<ScheduledGameDto>();
            foreach (var scheduledGame in scheduledGames)
            {
                teamGames.Add(ScheduledGameDto.Get(scheduledGame, await gamesQueryable
                    .Where(g => scheduledGame.Team1.Id == g.Winner.Id && scheduledGame.Team2.Id == g.Loser.Id ||
                                scheduledGame.Team2.Id == g.Winner.Id && scheduledGame.Team1.Id == g.Loser.Id)
                    .ToArrayAsync()));
            }

            return teamGames.ToArray();
        }
    }
}
