using System.Data.Entity;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GamePlay;

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

        public DbSet<PopulationDB> PopulationData
        {
            get;
            set;
        }
    }
}
