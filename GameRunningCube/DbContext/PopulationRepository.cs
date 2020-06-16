﻿using System.Collections.Generic;
using System.Linq;
using GameRunningCube.DbContext.Entities;

namespace GameRunningCube.DbContext
{
    public class PopulationRepository
    {
        public int MaxPopGenerationNumber { get; set; }
        public int MaxIdObject { get; set; } = 0;

        public List<PopulationDB> GetPopulationFromDb()
        {
            List<PopulationDB> pop = new List<PopulationDB>();
            using (var dbContext = new DbContextRunningCube())
            {
                MaxPopGenerationNumber = dbContext.PopulationData.Max(x => x.GenerationNumber);
                pop = dbContext.PopulationData.Where(x => x.GenerationNumber == MaxPopGenerationNumber).ToList();
                return pop;
            }
        }

        public List<PopulationDB> GetAllPopulation()
        {
            List<PopulationDB> pop = new List<PopulationDB>();
            using (var dbContext = new DbContextRunningCube())
            {
                var lit = dbContext.PopulationData.ToList();
                return lit;
            }
        }

        public PopulationDB GetBestPopulation()
        {
            return GetTopPopulationFromDb().OrderByDescending(x => x.Score).First();
        }

        public List<PopulationDB> GetTopPopulationFromDb()
        {
            using (var dbContext = new DbContextRunningCube())
            {

                var list = dbContext.PopulationData.Where(x => x.Score != 0).OrderByDescending(x => x.Score).Take(5);

                return list.ToList();
            }
        }

        public void UpdateScoreForCurrentPopulation(int playerAiCounter, int idObj, int score)
        {
            using (var dbContext = new DbContextRunningCube())
            {
                var population = dbContext.PopulationData.FirstOrDefault(x => x.IdObject == idObj);

                population.Score = score;
                population.MovesCount = playerAiCounter;
                dbContext.SaveChanges();
            }
        }

        public void SaveTrainedPopulation(int playerAiCounter, int currentPopulationIdObject, int scoreSpriteScore)
        {
            using (var dbContext = new DbContextRunningCube())
            {
                var population = dbContext.PopulationData.FirstOrDefault(x => x.IdObject == currentPopulationIdObject);

                population.Score = scoreSpriteScore;
                population.MovesCount = playerAiCounter;
                population.Trained = true;
                dbContext.SaveChanges();
            }
        }
    }
}