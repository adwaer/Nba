using Nba.Domain;
using Nba.Domain.Enums;

namespace Nba.Dal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Nba.Dal.DefaultCtx>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Nba.Dal.DefaultCtx";
        }

        protected override void Seed(Nba.Dal.DefaultCtx context)
        {
            if (!context.Seasons.Any())
            {
                context.Seasons.Add(new Season
                {
                    StartDate = new DateTime(2010, 10, 27),
                    EndDate = new DateTime(2011, 04, 14),
                    SeasonType = SeasonType.Regular,
                    IsCompleted = true,
                    Url = "https://www.championat.com/basketball/_nba/260/table/all.html"
                });
                //context.Seasons.Add(new Season
                //{
                //    StartDate = new DateTime(2011, 04, 16),
                //    EndDate = new DateTime(2011, 05, 02),
                //    SeasonType = SeasonType.PlayOff,
                //    IsCompleted = true,
                //    Url = "https://www.championat.com/basketball/_nba/346/table/all/playoff.html"
                //});

                context.Seasons.Add(new Season
                {
                    StartDate = new DateTime(2011, 12, 25),
                    EndDate = new DateTime(2012, 04, 19),
                    SeasonType = SeasonType.Regular,
                    IsCompleted = true,
                    Url = "https://www.championat.com/basketball/_nba/441/table/all.html"
                });
                //context.Seasons.Add(new Season
                //{
                //    StartDate = new DateTime(2012, 04, 28),
                //    EndDate = new DateTime(2012, 06, 22),
                //    SeasonType = SeasonType.PlayOff,
                //    IsCompleted = true,
                //    Url = "https://www.championat.com/basketball/_nba/514/table/all/playoff.html"
                //});

                context.Seasons.Add(new Season
                {
                    StartDate = new DateTime(2012, 10, 31),
                    EndDate = new DateTime(2013, 04, 18),
                    SeasonType = SeasonType.Regular,
                    IsCompleted = true,
                    Url = "https://www.championat.com/basketball/_nba/600/table/all.html"
                });
                //context.Seasons.Add(new Season
                //{
                //    StartDate = new DateTime(2013, 04, 21),
                //    EndDate = new DateTime(2013, 06, 21),
                //    SeasonType = SeasonType.PlayOff,
                //    IsCompleted = true,
                //    Url = "https://www.championat.com/basketball/_nba/731/table/all/playoff.html"
                //});

                context.Seasons.Add(new Season
                {
                    StartDate = new DateTime(2013, 10, 30),
                    EndDate = new DateTime(2014, 04, 17),
                    SeasonType = SeasonType.Regular,
                    IsCompleted = true,
                    Url = "https://www.championat.com/basketball/_nba/787/table/all.html"
                });
                //context.Seasons.Add(new Season
                //{
                //    StartDate = new DateTime(2014, 04, 18),
                //    EndDate = new DateTime(2014, 06, 16),
                //    SeasonType = SeasonType.PlayOff,
                //    IsCompleted = true,
                //    Url = "https://www.championat.com/basketball/_nba/993/table/all/playoff.html"
                //});

                context.Seasons.Add(new Season
                {
                    StartDate = new DateTime(2014, 10, 28),
                    EndDate = new DateTime(2015, 04, 15),
                    SeasonType = SeasonType.Regular,
                    IsCompleted = true,
                    Url = "https://www.championat.com/basketball/_nba/1048/table/all.html"
                });
                //context.Seasons.Add(new Season
                //{
                //    StartDate = new DateTime(2015, 04, 18),
                //    EndDate = new DateTime(2015, 06, 17),
                //    SeasonType = SeasonType.PlayOff,
                //    IsCompleted = true,
                //    Url = "https://www.championat.com/basketball/_nba/1249/table/all/playoff.html"
                //});

                context.Seasons.Add(new Season
                {
                    StartDate = new DateTime(2015, 10, 27),
                    EndDate = new DateTime(2016, 04, 14),
                    SeasonType = SeasonType.Regular,
                    IsCompleted = true,
                    Url = "https://www.championat.com/basketball/_nba/1667/table/all.html"
                });
                //context.Seasons.Add(new Season
                //{
                //    StartDate = new DateTime(2016, 04, 16),
                //    EndDate = new DateTime(2016, 06, 20),
                //    SeasonType = SeasonType.PlayOff,
                //    IsCompleted = true,
                //    Url = "https://www.championat.com/basketball/_nba/1742/table/all/playoff.html"
                //});
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
