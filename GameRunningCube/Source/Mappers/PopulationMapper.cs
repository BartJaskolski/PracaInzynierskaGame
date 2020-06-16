using System.Collections.Generic;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GeneticAlgorithm;

namespace GameRunningCube.Source.Mappers
{
    public class PopulationMapper
    {
        public void MapEntirePopToPopDb(List<Population> population)
        {
            using (var dbContext = new DbContextRunningCube())
            {
                foreach (var pop in population)
                {
                    dbContext.PopulationData.Add(new PopulationDB(pop));
                }

                dbContext.SaveChanges();
            }
        }
        

        public List<Population> MapEntirePopDbToPop(List<PopulationDB> getPopulationFromDb)
        {
            List<Population> populations = new List<Population>();
            
            foreach (var population in getPopulationFromDb)
            {
               populations.Add(new Population(population.IdObject, population.AiMoves, population.GenerationNumber,
                population.Score, population.MovesCount));
            }

            return populations;
        }

        public Population MapPopDbToPop(PopulationDB pop, int IdObj =0 , int maxPopGenerationNumber= 0)
        {
          return new Population(IdObj, pop.AiMoves, 0, maxPopGenerationNumber, pop.MovesCount);
        }
}

}
