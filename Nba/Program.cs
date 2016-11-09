using System;
using System.Diagnostics;
using System.Linq;
using Nba.Dal;
using Nba.Parser;

namespace Nba
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            args = new[] { "games_fetch" };
            //args = new[] { "teams_fetch", "games_fetch", "schedule_games_fetch" };
#endif

            var ctx = new DefaultCtx();

            if (args.Contains("teams_fetch"))
            {
                Console.WriteLine("teams_fetch");

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var fetchCmd = new FetchTeamsCommand(ctx);
                fetchCmd
                    .Execute()
                    .Wait();

                Console.WriteLine(
                    $"teams_fetch completed in: {stopwatch.Elapsed}");
                stopwatch.Stop();
            }

            if (args.Contains("schedule_games_fetch"))
            {
                Console.WriteLine("schedule_games_fetch");
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var gamesQuery = new ScheduledGamesQuery(ctx);
                var games = gamesQuery
                    .Execute();

                ctx.ScheduledGames.AddRange(games);
                ctx.SaveChanges();

                Console.WriteLine(
                    $"schedule_games_fetch completed in: {stopwatch.Elapsed}");
                stopwatch.Stop();
            }

            if (args.Contains("games_fetch"))
            {
                Console.WriteLine("games_fetch");
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                foreach (var season in ctx.Seasons.ToArray())
                {
                    Console.WriteLine($"start time: {stopwatch.Elapsed}");
                    stopwatch.Restart();

                    var games = new GamesQuery(ctx)
                        .Execute(season);

                    ctx.Games.AddRange(games);

                    Console.WriteLine(
                        $"parsed season: {season.Url}, game count: {games.Count}, time spent: {stopwatch.Elapsed}");
                }

                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            Console.WriteLine("This is the end");
            Console.ReadLine();
        }
    }
}
