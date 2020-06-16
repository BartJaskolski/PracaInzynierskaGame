namespace GameRunningCube.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movesCounter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PopulationDBs", "MovesCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PopulationDBs", "MovesCount");
        }
    }
}
