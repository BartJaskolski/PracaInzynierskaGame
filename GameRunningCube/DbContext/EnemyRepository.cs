using System;
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
    }
}
