namespace GameRunningCube.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnemyDBs",
                c => new
                    {
                        IdObject = c.Int(nullable: false, identity: true),
                        CorrelationID = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        PosX = c.Int(nullable: false),
                        PosY = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdObject);
            
            CreateTable(
                "dbo.PopulationDBs",
                c => new
                    {
                        IdObject = c.Int(nullable: false, identity: true),
                        AiMoves = c.String(),
                        Score = c.Int(nullable: false),
                        GenerationNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdObject);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PopulationDBs");
            DropTable("dbo.EnemyDBs");
        }
    }
}
