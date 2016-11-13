using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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
    }
}