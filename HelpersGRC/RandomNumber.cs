using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using GameRunningCube.Source.GamePlay;

namespace HelpersGRC
{
    public class RandomNumber
    {
        private readonly int MAX_PLAYER_MOVES = 1200;
        Random rand = new Random();

        public  int GenerateRandomNumber()
        {
            return rand.Next(1, 4);
        }

        public List<int> GenerateMovesForPlayer()
        {
            List<int> playersMoves = new List<int>(MAX_PLAYER_MOVES);

            for (int i = 0; i < MAX_PLAYER_MOVES; i++)
            {
                playersMoves.Add(this.GenerateRandomNumber()); 
            }

            return playersMoves;
        }
    }

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
