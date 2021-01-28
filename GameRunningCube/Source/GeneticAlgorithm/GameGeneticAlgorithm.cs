using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.Mappers;

namespace GameRunningCube.Source.GeneticAlgorithm
{
    public class GameGeneticAlgorithm
    {

        public GameGeneticAlgorithm(PopulationRepository rep, PopulationMapper popMappper)
        {
            Repository = rep;
            PopulationMapper = popMappper;
        }

        public PopulationRepository Repository { get; set; }
        public PopulationMapper PopulationMapper { get; set; }
        public List<PopulationDB> CurrentPop { get; set; }

        public void CrossOverPopulation(int numberOfCrossOver, List<Population> pop)
        {
            int popLilmit = pop.Count;
            for (int i = 0; i < numberOfCrossOver; i++)
            {
                var firstParent = GlobalVariables.Random.Next(0, popLilmit);
                var secondParent = GlobalVariables.Random.Next(0, popLilmit);

                pop.Add(MakeChild(pop[firstParent], pop[secondParent]));
            }

        }

        public string CrossOverParentsMovments(Population firstParentAiMoves, Population secondParentAiMoves)
        {
            //todo
            StringBuilder sb = new StringBuilder(200);

            if (firstParentAiMoves.MovesCount <= secondParentAiMoves.MovesCount)
            {
                sb.Append(secondParentAiMoves.AiMovesToStrong(secondParentAiMoves.AiMoves).Substring(0, secondParentAiMoves.MovesCount));
                sb.Append(firstParentAiMoves.AiMovesToStrong(firstParentAiMoves.AiMoves).Substring(secondParentAiMoves.MovesCount));
            }
            else
            {
                sb.Append(firstParentAiMoves.AiMovesToStrong(firstParentAiMoves.AiMoves).Substring(0, firstParentAiMoves.MovesCount));
                sb.Append(secondParentAiMoves.AiMovesToStrong(secondParentAiMoves.AiMoves).Substring(firstParentAiMoves.MovesCount));

            }
        
            return sb.ToString();
        }

        public List<Population> SelectionFromCurrentGeneration(int count, List<Population> pop)
        {
            CurrentPop = Repository.GetPopulationFromDb();
            Repository.MaxIdObject = PopulationDB.GetMaxIdObject();
            Repository.MaxPopGenerationNumber++;
            pop.Clear();

            var top = Repository.GetTopPopulationFromDb();

            foreach (var populationDb in top)
            {
                pop.Add(PopulationMapper.MapPopDbToPop(populationDb, Repository.MaxIdObject, Repository.MaxPopGenerationNumber));
                Repository.MaxIdObject++;
                count--;
            }


            for (int j = 0; j < count / 2; j++)
            {
                var AllPop = Repository.GetAllPopulation();

                var firstPlayer = GlobalVariables.Random.Next(0, AllPop.Count);
                var secondPlayer = GlobalVariables.Random.Next(0, AllPop.Count);


                    if (AllPop[firstPlayer].Score > AllPop[secondPlayer].Score)
                    {
                        pop.Add(PopulationMapper.MapPopDbToPop(AllPop[firstPlayer], Repository.MaxIdObject, Repository.MaxPopGenerationNumber));
                    }
                    else
                    {
                        pop.Add(PopulationMapper.MapPopDbToPop(AllPop[secondPlayer], Repository.MaxIdObject, Repository.MaxPopGenerationNumber));
                    }

                Repository.MaxIdObject++;
            }

            for (int i = 0; i < (count/2); i++)
            {
                var firstPlayer = GlobalVariables.Random.Next(0, CurrentPop.Count);
                var secondPlayer = GlobalVariables.Random.Next(0, CurrentPop.Count);


                if (CurrentPop[firstPlayer].Score > CurrentPop[secondPlayer].Score)
                {
                    pop.Add(PopulationMapper.MapPopDbToPop(CurrentPop[firstPlayer], Repository.MaxIdObject, Repository.MaxPopGenerationNumber));
                }
                else
                {
                    pop.Add(PopulationMapper.MapPopDbToPop(CurrentPop[secondPlayer], Repository.MaxIdObject, Repository.MaxPopGenerationNumber));
                }

                Repository.MaxIdObject++;
            }

            return pop;
        }

        public void MutatePopulation(List<Population> population)
        {
            int x = GlobalVariables.Random.Next();
            foreach (var pop in population)
            {
                for (int i = 0; i < pop.AiMoves.Count/3; i++)
                {
                    x = GlobalVariables.Random.Next(pop.MovesCount, 200);
                    var rand = GlobalVariables.Random.NextDouble();
                    if (rand <= .05d)
                    {
                        
                        int oldValue = pop.AiMoves[x];
                        int newValue = 9;

                        switch (oldValue)
                        {
                            case 1:
                                {
                                    double rand2 = GlobalVariables.Random.NextDouble();
                                    if (rand2 <= .5d)
                                        newValue = 2;
                                    else
                                        newValue = 3;
                                }
                                break;
                            case 2:
                                {
                                    double rand2 = GlobalVariables.Random.NextDouble();
                                    if (rand2 <= .5d)
                                        newValue = 1;
                                    else
                                        newValue = 3;
                                }
                                break;
                            case 3:
                                {
                                    double rand2 = GlobalVariables.Random.NextDouble();
                                    if (rand2 <= .5d)
                                        newValue = 2;
                                    else
                                        newValue = 1;
                                }
                                break;
                        }

                        pop.Mutations++;
                        pop.AiMoves[x] = newValue;
                    }
                }
            }

            PopulationMapper.MapEntirePopToPopDb(population);
        }

        private Population MakeChild(Population firstParent, Population secondParent)
        {
            Repository.MaxIdObject++;

            var newChlid = new Population(Repository.MaxIdObject, CrossOverParentsMovments(firstParent, secondParent), 0, Repository.MaxPopGenerationNumber, 0);

            return newChlid;
        }
    }
}
