using System.Data.Entity;
using GameRunningCube.DbContext.Entities;

namespace GameRunningCube.DbContext
{
    public class DbContextRunningCube : System.Data.Entity.DbContext
    {
        public DbContextRunningCube(string name = "name=RunningCubeConn") : base(name)
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

        public DbSet<ParametersDB> Parameters { get; set; }
    }
  
}
