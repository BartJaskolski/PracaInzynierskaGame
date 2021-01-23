﻿using System.Collections.Generic;
using System.Linq;
using GameRunningCube.DbContext;
using GameRunningCube.Source.GameEngine;
using GameRunningCube.Source.GamePlay;
using GameRunningCube.Source.GeneticAlgorithm;
using GameRunningCube.Source.Mappers;
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

        public GameGeneticAlgorithm GeneticAlgorithm { get; set; }

        //DbRepositories
        #region MyRegion

        public PopulationRepository PopulationRepository { get; set; }
        public EnemyRepository EnemyRepository { get; set; }


        #endregion

        //Mappers
        #region MyRegion

        public PopulationMapper PopulationMapper { get; set; }
        public EnemyMapper EnemyMapper { get; set; }

        #endregion

        public bool StopGame { get; set; }
        public bool AllPopulationAfterGame => Population.All(x => x.AfterGame != false);
        public int MaxIdObject { get; set; }

        public int MaxPopGenerationNumber => PopulationRepository.MaxPopGenerationNumber;
        public GameBoard(GameSettings settings) : this()
        {
            if (settings.Tryb == GameMode.Test)
                SetDefaultTest();
                
            else
                SetDefaultValues();
            
        }

        public GameBoard()
        {
            EnemyMapper = new EnemyMapper();
            EnemyRepository = new EnemyRepository(EnemyMapper);
            PopulationMapper = new PopulationMapper();
            PopulationRepository = new PopulationRepository();
            GeneticAlgorithm = new GameGeneticAlgorithm(PopulationRepository, PopulationMapper);

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

            Player.Update();
            ScoreSprite.Update();

            if (Player.AiCounter == 200 && !Player.Lost)
            {
                StopGame = true;
                PopulationRepository.SaveTrainedPopulation(Player.AiCounter, CurrentPopulation.IdObject, ScoreSprite.score);
            }

            if (Player.AiCounter == 200 || Player.Lost)
            {
                var popAfterGame= Population.First(x => !CurrentPopulation.AfterGame  && x.IdObject == CurrentPopulation.IdObject && x.AfterGame== false);
                popAfterGame.AfterGame = true;
                popAfterGame.MovesCount = Player.AiCounter;

                PopulationRepository.UpdateScoreForCurrentPopulation(Player.AiCounter, CurrentPopulation.IdObject,ScoreSprite.score);

                if(!AllPopulationAfterGame)
                    GetNextPopulation(MaxPopGenerationNumber);
                else
                {
                    if (CheckIfStopAlgorithm())
                    {
                        StopGame = true;
                    }
                    else
                    {
                        int numberOfParents = Population.Count / 2;
                        int numberOfChildren = Population.Count - numberOfParents;
                        var population = GeneticAlgorithm.SelectionFromCurrentGeneration(numberOfParents, Population);
                        GeneticAlgorithm.CrossOverPopulation(numberOfChildren, population);
                        GeneticAlgorithm.MutatePopulation(population);
                        GetNextPopulation(MaxPopGenerationNumber);
                    }
                }
            }

            if (CurrentPopulation != null && !Player.LoadedAiData)
                LoadAiData(Player);

            foreach (var enemy in Enemies)
                enemy.Update();
        }

        private bool CheckIfStopAlgorithm()
        {
            return false;
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
            Player = new Player(new Vector2(300, 600), new Vector2(19, 19), "2D\\Player");
            Population = PopulationMapper.MapEntirePopDbToPop(PopulationRepository.GetPopulationFromDb());
            ScoreSprite = new ScoreSprite(Player, PopulationRepository.MaxPopGenerationNumber);
            Enemies = EnemyRepository.GetEnemiesFromDb();
            CurrentPopulation = Population.First(x => x.AfterGame == false);
        }

        private void SetDefaultTest()
        {
            Player = new Player(new Vector2(300, 600), new Vector2(19, 19), "2D\\Player");
            ScoreSprite = new ScoreSprite(Player, 0);
            Population = PopulationMapper.MapEntirePopDbToPop(PopulationRepository.GetPopulationFromDb());
            Enemies = EnemyRepository.GetEnemiesFromDb();
            CurrentPopulation =PopulationMapper.MapPopDbToPop(PopulationRepository.GetBestPopulation());
        }

        private void GetNextPopulation(int maxPopGenerationNumber)
        {
            Player = new Player(new Vector2(300, 600), new Vector2(20, 20), "2D\\Player");
            ScoreSprite = new ScoreSprite(Player,maxPopGenerationNumber);
            Enemies = EnemyRepository.GetEnemiesFromDb();
            CurrentPopulation = Population.First(x => x.AfterGame == false);
        }
    }
}
