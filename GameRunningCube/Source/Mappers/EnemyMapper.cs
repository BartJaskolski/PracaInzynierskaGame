using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GamePlay;

namespace GameRunningCube.Source.Mappers
{
    public class EnemyMapper
    {
        public List<Enemy> MapDbToObj(List<EnemyDB> enemyData)
        {
            List<Enemy> enemies = new List<Enemy>();

            foreach (var enemyDb in enemyData)
            {
                enemies.Add(GlobalVariables.ObjectGenerator.ConvertDbToObj<Enemy>(enemyDb));
            }

            return enemies;
        }
    }


}
