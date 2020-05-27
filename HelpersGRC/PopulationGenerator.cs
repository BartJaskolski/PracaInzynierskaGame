using System.Collections.Generic;
using GameRunningCube.Source.GamePlay;

namespace HelpersGRC
{

    public class PopulationGenerator
    {
        public List<Player> GeneratedPopulation { get; set; }
        private RandomNumber NumberGenerator { get; set; }

        public PopulationGenerator()
        {
            NumberGenerator = new RandomNumber();
            GeneratedPopulation = GeneratePopulation();
        }

        private List<Player> GeneratePopulation()
        {
            List<Player> populationPlayer = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                var player = new Player();
                player.AiMoves = NumberGenerator.GenerateMovesForPlayer();
                populationPlayer.Add(player);
            }
            return populationPlayer;
        }
    }
}
