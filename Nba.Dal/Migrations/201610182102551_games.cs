namespace Nba.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class games : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Url = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        WinnerScore = c.Int(nullable: false),
                        LoserScore = c.Int(nullable: false),
                        Team_Id = c.String(maxLength: 128),
                        Loser_Id = c.String(maxLength: 128),
                        Season_Id = c.Int(),
                        Winner_Id = c.String(maxLength: 128),
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
                        Game_Id = c.String(maxLength: 128),
                        Loser_Id = c.String(maxLength: 128),
                        Winner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.Teams", t => t.Loser_Id)
                .ForeignKey("dbo.Teams", t => t.Winner_Id)
                .Index(t => t.Game_Id)
                .Index(t => t.Loser_Id)
                .Index(t => t.Winner_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Winner_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Games", "Loser_Id", "dbo.Teams");
            DropForeignKey("dbo.GameParts", "Winner_Id", "dbo.Teams");
            DropForeignKey("dbo.GameParts", "Loser_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.GameParts", "Game_Id", "dbo.Games");
            DropIndex("dbo.GameParts", new[] { "Winner_Id" });
            DropIndex("dbo.GameParts", new[] { "Loser_Id" });
            DropIndex("dbo.GameParts", new[] { "Game_Id" });
            DropIndex("dbo.Games", new[] { "Winner_Id" });
            DropIndex("dbo.Games", new[] { "Season_Id" });
            DropIndex("dbo.Games", new[] { "Loser_Id" });
            DropIndex("dbo.Games", new[] { "Team_Id" });
            DropTable("dbo.Seasons");
            DropTable("dbo.GameParts");
            DropTable("dbo.Games");
        }
    }
}
