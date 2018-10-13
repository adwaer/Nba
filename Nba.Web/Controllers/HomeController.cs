using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nba.Dal;
using Nba.Domain;
using Nba.Parser;
using Nba.Queries;
using Nba.Queries.Criteria;
using Nba.Web.Models;

namespace Nba.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> HistoryStat(BetsForkCondition condition)
        {
            var winlineGames = await new OnlineGamesQuery()
                .Execute(condition?.FonUrl ?? ConfigurationManager.AppSettings["fon"]);

            return View(new BetForkViewModel
            {
                Condition = condition
            });
        }

        public async Task<ActionResult> Index(DayGamesCondition condition)
        {
            condition = condition ?? new DayGamesCondition();
            condition.Date = condition.Date ?? DateTime.Now;

            var games = await new DayGamesQuery()
                .Execute(condition);

            var seasons = await new SeasonsQuery().Execute();
            if (!condition.SeasonId.HasValue)
            {
                condition.SeasonId = seasons.OrderBy(s => s.Id).FirstOrDefault()?.Id;
            }

            return View(new ScheduleViewModel
            {
                Games = games,
                Condition = condition,
                Seasons = seasons
            });
        }

        public async Task<ActionResult> FetchOpenedSeason()
        {
            var defaultCtx = new DefaultCtx();

            var season = await defaultCtx.Seasons
                .FirstOrDefaultAsync(s => !s.IsCompleted);

            if (season == null)
            {
                return View(new FetchGamesViewModel { Result = "Нет открытых сезонов" });
            }

            var dbGames = await defaultCtx
                .Games
                .ToArrayAsync();

            var games = new GamesQuery(defaultCtx)
                .Execute(season)
                .Where(dbg => dbGames.All(g => g.Url != dbg.Url));

            defaultCtx.Games.AddRange(games);
            defaultCtx.SaveChanges();

            return View(new FetchGamesViewModel { Result = "Ok" });
        }
    }
}