namespace GameRunningCube.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addKey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParametersDBs",
                c => new
                    {
                        IdObject = c.Int(nullable: false, identity: true),
                        SpeedOfAGame = c.Double(nullable: false),
                        AmountOfPopulation = c.Int(nullable: false),
                        MutationPercent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.IdObject);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParametersDBs");
        }
    }
}
