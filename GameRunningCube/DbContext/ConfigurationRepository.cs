using System.Collections.Generic;
using System.Linq;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GamePlay;

namespace GameRunningCube.DbContext
{
    public class ConfigurationRepository
    {

        public ConfigurationRepository()
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
                var parameters = dbContext.Parameters.FirstOrDefault();
                return parameters;
            }
        }
    }
}
