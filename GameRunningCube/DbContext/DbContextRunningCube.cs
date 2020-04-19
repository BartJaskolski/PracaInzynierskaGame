using System.Data.Entity;
using GameRunningCube.Source.GamePlay.Board;

namespace GameRunningCube.DbContext
{
    public class DbContextRunningCube : System.Data.Entity.DbContext
    {
        public DbContextRunningCube() : base("name=RunningCubeConn")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DbContextRunningCube>());
        }

        public DbSet<EnemyDB> EnemiesData
        {
            get;
            set;
        }
    }
}
