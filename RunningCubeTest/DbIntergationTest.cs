using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace RunningCubeTest
{
    [TestClass]
    public class DbIntergationTest
    {
        [TestMethod]
        public void SaveParametersRepoTest()
        {
            var context = new DbContextRunningCube("RunningCubeTest");

            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [ParametersDBs]");

            context.Parameters.Add(new ParametersDB(1, 1 , 1));
            context.SaveChanges();

            var param = context.Parameters.Count(x => x.SpeedOfAGame == 1 && x.AmountOfPopulation == 1 && x.MutationPercent == 1);

            Assert.IsTrue(param == 1);
        }

        [TestMethod]
        public void SaveGeneratedPopulationRepoTest()
        {
            var context = new DbContextRunningCube("RunningCubeTest");

            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [PopulationDBs]");
            context.PopulationData.AddRange(new List<PopulationDB>()
            {
                new PopulationDB("123",0),
                new PopulationDB("345",1),
            } );
            context.SaveChanges();

            var param = context.PopulationData.Count(x=> x.AiMoves == "123" || x.AiMoves == "345");

            Assert.IsTrue(param == 2);
        }

        [TestMethod]
        public void SaveGeneratedEnemiesRepoTest()
        {
            var context = new DbContextRunningCube("RunningCubeTest");

            context.Database.ExecuteSqlCommand("TRUNCATE TABLE [EnemyDBs]");
            context.EnemiesData.AddRange(new List<EnemyDB>()
            {
                new EnemyDB(0,10,10,10,10),
                new EnemyDB(0,20,20,20,20),
            });
            context.SaveChanges();

            var enemies = context.EnemiesData;

            Assert.IsTrue(enemies.Count(x => x.CorrelationID == 0) == 2);
            Assert.IsTrue(enemies.Any(x=>x.Height==20));

        }
    }
}
