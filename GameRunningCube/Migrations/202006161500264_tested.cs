namespace GameRunningCube.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tested : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PopulationDBs", "Trained", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PopulationDBs", "Trained");
        }
    }
}
