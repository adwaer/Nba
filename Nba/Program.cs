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
            var ctx = new DefaultCtx();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var season in ctx.Seasons.ToArray())
            {
                var games = new GamesQuery(ctx)
                    .Execute(season);

                ctx.Games.AddRange(games);

                Console.WriteLine($"parsed season: {season.Url}, game count: {games.Count}, time spent: {stopwatch.Elapsed}");
                stopwatch.Restart();
            }
            ctx.SaveChanges();

            if (args.Contains("teams_fetch"))
            {
                var fetchCmd = new FetchTeamsCommand(ctx);
                var teams = fetchCmd.Execute();
            }

            Console.WriteLine("This is the end");
            Console.ReadLine();
        }
    }
}
