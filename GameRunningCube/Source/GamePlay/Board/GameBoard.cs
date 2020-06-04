using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GamePlay;
using GameRunningCube.Source.GeneticAlgorithm;
using Microsoft.Xna.Framework;

namespace GameRunningCube
{
    public class GameBoard
    {
        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; }
        public ScoreSprite ScoreSprite { get; set; }
        public List<Population> Population { get; set; }
        public Population CurrentPopulation { get; set; }
        public int MaxPopGenerationNumber { get; set; }
        public bool StopGame { get; set; }
        public bool AllPopulationAfterGame => Population.All(x => x.AfterGame != false);
        public int MaxIdObject { get; set; }

        public GameBoard()
        {
            SetDefaultValues();
        }

        public virtual void Draw()
        {
            Player.Draw();
            ScoreSprite.Draw();

            foreach (var enemy in Enemies)
                enemy.Draw();
        }

        public virtual void Update()
        {
            if (GlobalVariables.KeyboardController.GetPress("R"))
                Restart();

            if (Player.AiCounter == 1200 || Player.Lost)
            {
                Population.FirstOrDefault(x => !CurrentPopulation.AfterGame  && x.IdObject == CurrentPopulation.IdObject).AfterGame = true;
                UpdateScoreForCurrentPopulation();

                if(!AllPopulationAfterGame)
                    GetNextPopulation();
                else
                {

                    //sprawdzenie czy dana populacja osiągneła cel.
                    if (CheckIfStopAlgorithm())
                    {
                        StopGame = true;
                    }
                    else
                    {
                        int numberOfParents = Population.Count / 2;
                        int numberOfChildren = Population.Count - numberOfParents;
                        var population= SelectionFromCurrentGeneration(numberOfParents);
                        CrossOverPopulation(numberOfChildren, population);
                        MutatePopulation();
                        GetNextPopulation();
                    }
                    //Populacja sprawdzona 
                    //Operacje Krzyżownia i mutacji
                    // utworzenie nowej populacji
                }
            }

            if (CurrentPopulation != null && !Player.LoadedAiData )
                LoadAiData(Player);

            Player.Update();
            ScoreSprite.Update();

            foreach (var enemy in Enemies)
                enemy.Update();
        }

        private void MutatePopulation()
        {
            //to implement
        }

        private void CrossOverPopulation(int numberOfCrossOver, List<Population> pop)
        {
            var population = GetPopulationFromDb();
            
            for (int i = 0; i < numberOfCrossOver;i++)
            {
                var firstParent = GlobalVariables.Random.Next(0, population.Count - 1);
                var secondParent = GlobalVariables.Random.Next(0, population.Count - 1);

                pop.Add(MakeChild(population[firstParent],population[secondParent]));
            }

            pop.ForEach(x => x.AfterGame = false);
            MapEntirePopToPopDb(pop);
        }

        private Population MakeChild(PopulationDB firstParent, PopulationDB secondParent)
        {
            MaxIdObject++;
            var newChlid = new Population(MaxIdObject, CrossOverParentsMovments(firstParent.AiMoves, secondParent.AiMoves),0, MaxPopGenerationNumber);

            return newChlid;
        }

        private string CrossOverParentsMovments(string firstParentAiMoves, string secondParentAiMoves)
        {
            //todo
            StringBuilder sb = new StringBuilder(1200);

            for (int i = 0; i < 1200; i+=20)
            {
                sb.Append(firstParentAiMoves.Substring(i, 10));
                sb.Append(secondParentAiMoves.Substring(i+10, 10));
            }

            return sb.ToString();
        }

        private List<Population> SelectionFromCurrentGeneration(int count)
        {
            var population = GetPopulationFromDb();
            MaxIdObject = PopulationDB.GetMaxIdObject();
            MaxPopGenerationNumber++;
            Population.Clear();

            for (int i = 0; i < count; i++)
            {
                var firstPlayer = GlobalVariables.Random.Next(0, population.Count-1);
                var secondPlayer = GlobalVariables.Random.Next(0, population.Count-1);

                if (population[firstPlayer].Score > population[secondPlayer].Score)
                {
                    Population.Add(MapPopDbToPop(population[firstPlayer], MaxIdObject));
                }
                else
                {
                    Population.Add(MapPopDbToPop(population[secondPlayer], MaxIdObject));
                }

                MaxIdObject++;
            }

            return Population;
        }

        private void MapEntirePopToPopDb(List<Population> population)
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

        private bool CheckIfStopAlgorithm()
        {
            return false;
        }

        private void UpdateScoreForCurrentPopulation()
        {
            using (var dbContext = new DbContextRunningCube())
            {
                var population = dbContext.PopulationData.FirstOrDefault(x => x.IdObject == CurrentPopulation.IdObject);
                population.Score = ScoreSprite.score;
                dbContext.SaveChanges();
            }
        }

        private void LoadAiData(Player player)
        {
            player.AiMoves = CurrentPopulation.AiMoves;
            player.LoadedAiData = true;
        }

        private void Restart()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Player = new Player(new Vector2(300, 600), new Vector2(30, 30), "2D\\Player");
            ScoreSprite = new ScoreSprite(Player, MaxPopGenerationNumber);
            Enemies = GetEnemiesFromDb();
            Population = MapEntirePopDbToPop(GetPopulationFromDb());
            CurrentPopulation = Population.First(x => x.AfterGame == false);
            //Enemies = new List<Enemy>();
            //for (int i = 0; i < 10; i++)
            //{
            //    Enemies.Add(GlobalVariables.ObjectGenerator.GenerateRandomObject<Enemy>("2D\\Enemy"));
            //} 
        }

        private void GetNextPopulation()
        {
            Player = new Player(new Vector2(300, 600), new Vector2(30, 30), "2D\\Player");
            ScoreSprite = new ScoreSprite(Player,MaxPopGenerationNumber);
            Enemies = GetEnemiesFromDb();
            CurrentPopulation = Population.First(x => x.AfterGame == false);
        }

        private List<Population> MapEntirePopDbToPop(List<PopulationDB> getPopulationFromDb)
        {
            List<Population> populations = new List<Population>();
            foreach (var population in getPopulationFromDb)
            {
                populations.Add(new Population(population.IdObject,population.AiMoves, population.GenerationNumber, population.Score));
            }

            return populations;
        }

        private Population MapPopDbToPop(PopulationDB pop,int IdObj)
        {
            return new Population(IdObj, pop.AiMoves,  0, MaxPopGenerationNumber);
        }

        private List<PopulationDB> GetPopulationFromDb()
        {
            List < PopulationDB > pop = new List<PopulationDB>();
            using (var dbContext = new DbContextRunningCube())
            {
                MaxPopGenerationNumber = dbContext.PopulationData.Max(x=>x.GenerationNumber);
                pop = dbContext.PopulationData.Where(x => x.GenerationNumber == MaxPopGenerationNumber).ToList();
                return pop;
            }
        }

        private List<Enemy> GetEnemiesFromDb()
        {
            DbContextRunningCube dbContext = new DbContextRunningCube();
            List<EnemyDB> enemyData = dbContext.EnemiesData.ToList();
            
            return MapDbToObj(enemyData);
        }

        private List<Enemy> MapDbToObj(List<EnemyDB> enemyData)
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
