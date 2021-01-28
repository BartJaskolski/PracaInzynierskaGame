using System.Collections.Generic;
using GameRunningCube.Source.GameEngine;
using GameRunningCube.Source.GamePlay;

namespace HelpersGRC
{

    public class PopulationGenerator
    {
        public List<GameRunningCube.Source.GamePlay.Player> GeneratedPopulation { get; set; }
        private RandomNumber NumberGenerator { get; set; }
        public GameSettings Settings { get; set; }

        public PopulationGenerator(GameSettings settings)
        {
            Settings = settings;
            NumberGenerator = new RandomNumber();
            GeneratedPopulation = GeneratePopulation();
        }

        private List<GameRunningCube.Source.GamePlay.Player> GeneratePopulation()
        {
            List<Player> populationPlayer = new List<Player>();
            for (int i = 0; i < Settings.AmountOfPopulation; i++)
            {
                var player = new Player();
                player.AiMoves = NumberGenerator.GenerateMovesForPlayer();
                populationPlayer.Add(player);
            }
            return populationPlayer;
        }
    }
}
