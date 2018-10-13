namespace Nba.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        WinnerScore = c.Int(nullable: false),
                        LoserScore = c.Int(nullable: false),
                        Team_Id = c.Int(),
                        Loser_Id = c.Int(),
                        Season_Id = c.Int(),
                        Winner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .ForeignKey("dbo.Teams", t => t.Loser_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .ForeignKey("dbo.Teams", t => t.Winner_Id)
                .Index(t => t.Team_Id)
                .Index(t => t.Loser_Id)
                .Index(t => t.Season_Id)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.GameParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WinnerScore = c.Int(nullable: false),
                        LoserScore = c.Int(nullable: false),
                        Game_Id = c.Int(),
                        Loser_Id = c.Int(),
                        Winner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.Teams", t => t.Loser_Id)
                .ForeignKey("dbo.Teams", t => t.Winner_Id)
                .Index(t => t.Game_Id)
                .Index(t => t.Loser_Id)
                .Index(t => t.Winner_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        SeasonType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScheduledGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Date = c.DateTime(nullable: false),
                        Team1_Id = c.Int(),
                        Team2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team1_Id)
                .ForeignKey("dbo.Teams", t => t.Team2_Id)
                .Index(t => t.Team1_Id)
                .Index(t => t.Team2_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduledGames", "Team2_Id", "dbo.Teams");
            DropForeignKey("dbo.ScheduledGames", "Team1_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "Winner_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Games", "Loser_Id", "dbo.Teams");
            DropForeignKey("dbo.GameParts", "Winner_Id", "dbo.Teams");
            DropForeignKey("dbo.GameParts", "Loser_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.GameParts", "Game_Id", "dbo.Games");
            DropIndex("dbo.ScheduledGames", new[] { "Team2_Id" });
            DropIndex("dbo.ScheduledGames", new[] { "Team1_Id" });
            DropIndex("dbo.GameParts", new[] { "Winner_Id" });
            DropIndex("dbo.GameParts", new[] { "Loser_Id" });
            DropIndex("dbo.GameParts", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "Winner_Id" });
            DropIndex("dbo.Games", new[] { "Season_Id" });
            DropIndex("dbo.Games", new[] { "Loser_Id" });
            DropIndex("dbo.Games", new[] { "Team_Id" });
            DropTable("dbo.ScheduledGames");
            DropTable("dbo.Seasons");
            DropTable("dbo.Teams");
            DropTable("dbo.GameParts");
            DropTable("dbo.Games");
        }
    }
}
