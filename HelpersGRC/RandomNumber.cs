using System;
using System.Collections.Generic;
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
}
