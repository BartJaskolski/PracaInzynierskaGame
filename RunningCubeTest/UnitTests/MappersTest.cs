using GameRunningCube;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GamePlay;
using GameRunningCube.Source.GeneticAlgorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RunningCubeTest
{
    [TestClass]
    public class MappersTest
    {
        [TestMethod]
        public void EnemyMapperTest()
        {
            EnemyDB enemyDb = new EnemyDB(1,1,1,1,1);

            Enemy enemy = new GameRunningCube.Source.Helpers.ObjectGenerator().ConvertDbToObj<Enemy>(enemyDb);

            Assert.IsTrue(enemy.Dimention.X == enemyDb.Width && enemy.Dimention.Y == enemyDb.Height);
            Assert.IsTrue(enemy.Location.X == enemyDb.PosX && enemy.Location.Y == enemyDb.PosY);
        }

        [TestMethod]
        public void PopulationListToDBMapperTest()
        {
            List<Population> pop = new List<Population>() {
                new Population(999999, "2131", 0, 0, 1)
            };

            PopulationDB mappedPop = new PopulationDB(pop.First());

            Assert.IsTrue(mappedPop.IdObject == pop.First().IdObject && mappedPop.Score == pop.First().Score);
            Assert.IsTrue(mappedPop.GenerationNumber == pop.First().GenerationNumber && mappedPop.MovesCount == pop.First().MovesCount);
        }

        [TestMethod]
        public void DBtoPopulationListMapperTest()
        {
            List<PopulationDB> pop = new List<PopulationDB>() {
                new PopulationDB("2131",1)
                {
                    IdObject =1,
                    Score =1,
                    MovesCount =0
                }
            };

            Population mappedPop = new Population(pop.First().IdObject,
                pop.First().AiMoves,
                pop.First().Score,
                pop.First().GenerationNumber,
                pop.First().MovesCount);

            Assert.IsTrue(mappedPop.IdObject == pop.First().IdObject && mappedPop.Score == pop.First().Score);
            Assert.IsTrue(mappedPop.GenerationNumber == pop.First().GenerationNumber && mappedPop.MovesCount == pop.First().MovesCount);

        }

    }
}
