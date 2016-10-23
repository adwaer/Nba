namespace Nba.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Abbr = c.String(),
                        City = c.String(),
                        CityEn = c.String(),
                        Code = c.String(),
                        DisplayAbbr = c.String(),
                        DisplayConference = c.String(),
                        IsAllStarTeam = c.Boolean(nullable: false),
                        IsLeagueTeam = c.Boolean(nullable: false),
                        LeagueId = c.String(),
                        Name = c.String(),
                        NameEn = c.String(),
                        Conference_Id = c.Int(),
                        Division_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conferences", t => t.Conference_Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id)
                .Index(t => t.Conference_Id)
                .Index(t => t.Division_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.Teams", "Conference_Id", "dbo.Conferences");
            DropIndex("dbo.Teams", new[] { "Division_Id" });
            DropIndex("dbo.Teams", new[] { "Conference_Id" });
            DropTable("dbo.Teams");
            DropTable("dbo.Divisions");
            DropTable("dbo.Conferences");
        }
    }
}
