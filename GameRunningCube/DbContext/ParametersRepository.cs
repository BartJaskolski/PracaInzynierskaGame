using System.Collections.Generic;
using System.Linq;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GamePlay;

namespace GameRunningCube.DbContext
{
    public class ParametersRepository
    {

        public ParametersRepository()
        {

        }

        public void SaveParameters(double mutaionPercent, double speedOfame, int amoutOfPopulation)
        {
            using (var db = new DbContextRunningCube())
            {
                db.Parameters.Add(new ParametersDB(mutaionPercent, speedOfame
                    , amoutOfPopulation));
                db.SaveChanges();
            }
        }

        public ParametersDB GetParameters()
        {
            using (var dbContext = new DbContextRunningCube())
            {
                var parameters = dbContext.Parameters.ToList();
                return parameters.OrderByDescending(x=>x.IdObject).First();
            }
        }

        public void ClearEnemies()
        {
            using (var db = new DbContextRunningCube())
            {
                if (db.Parameters.Any())
                {
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE [ParametersDBs]");
                    db.SaveChanges();
                }
            }
        }
    }
}
