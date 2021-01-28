using GameRunningCube;
using GameRunningCube.Source.GameEngine;
using GameRunningCube.Source.GeneticAlgorithm;
using GameRunningCube.Source.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RunningCubeTest
{
    [TestClass]
    public class GeneticAlgorithmTest
    {
        [TestMethod]
        public void MakeChildTest()
        {
            GameGeneticAlgorithm gameGenetic = new GameGeneticAlgorithm(
                new GameRunningCube.DbContext.PopulationRepository(),
                new PopulationMapper(),
                new GameSettings()
                );

            Population first = new Population(1,"1111",1,0, 2);
            Population sec = new Population(1, "0000", 0, 0, 3);

           var result = gameGenetic.MakeChild(first, sec);

            Assert.IsTrue(result.AiMoves.Count == 4);
            Assert.IsTrue(IsListValuesEquals(result.AiMoves, new List<int>{ 0, 0, 0, 1 }));
        }

        private bool IsListValuesEquals<T>(List<T> a, List<T> b)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if(!EqualityComparer<T>.Default.Equals(a[i],b[i]))
                    return false;
            }
            return true;
        }

        [TestMethod]
        public void MutatePopulationTest()
        {
            GameGeneticAlgorithm gameGenetic = new GameGeneticAlgorithm(
                  new GameRunningCube.DbContext.PopulationRepository(),
                  new PopulationMapper(),
                  new GameSettings() { MuationPercent = 100}
                  );
            GlobalVariables.Random = new Random(10);
            List<Population> pop = new List<Population>() {
                new Population(999999, "2131", 0, 0, 1),
                new Population(999998, "2231", 0, 0, 2),
                new Population(999997, "2223", 0, 0, 3)
            };
            
            var pop2 = gameGenetic.MutatePopulation(pop);
            int sum = 0;
            pop2.ForEach(x => {
                sum += x.Mutations;
            } );
            int result = pop2.Count * pop.First().AiMoves.Count / 3;

            Assert.IsNotNull(pop2[1]);
            Assert.AreEqual(sum, result);
            Assert.AreNotEqual(pop[1], pop2[1]);

        }

    }
}
