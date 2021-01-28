using System.Collections.Generic;
using System.Linq;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GamePlay;
using GameRunningCube.Source.Mappers;

namespace GameRunningCube.DbContext
{
    public class EnemyRepository
    {
        public EnemyMapper Mapper { get; set; }

        public EnemyRepository(EnemyMapper mapper)
        {
            Mapper = mapper;
        }

        public List<Enemy> GetEnemiesFromDb()
        {
            DbContextRunningCube dbContext = new DbContextRunningCube();
            List<EnemyDB> enemyData = dbContext.EnemiesData.ToList();

            return Mapper.MapDbToObj(enemyData);
        }

        public void SaveGeneratedEnemies(List<EnemyDB> enemies) 
        {

            using (var db = new DbContextRunningCube())
            {
                db.EnemiesData.AddRange(enemies);
                db.SaveChanges();
            }
        }

        public void ClearEnemies()
        {
            using (var db = new DbContextRunningCube())
            {
                if (db.EnemiesData.Any())
                {
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE [EnemyDBs]");
                    db.SaveChanges();
                }
            }
        }
    }
}
