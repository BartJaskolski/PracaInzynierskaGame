using System;
using System.Collections.Generic;
using GameRunningCube;
using GameRunningCube.Source.GamePlay;

namespace HelpersGRC
{
    public class RandomNumber
    {
        private readonly int MAX_PLAYER_MOVES = 1200;

        public  int GenerateRandomNumber()
        {
            return GlobalVariables.Random.Next(1, 3);
        }

        public List<int> GenerateMovesForPlayer()
        {
            List<int> playersMoves = new List<int>(MAX_PLAYER_MOVES);

            for (int i = 0; i < MAX_PLAYER_MOVES; i++)
            {
                int value;
                double rand = GlobalVariables.Random.NextDouble();
                if (rand <= .45d)
                    value = GlobalVariables.Random.Next(1, 4);
                else
                {
                    double rand2 = GlobalVariables.Random.NextDouble();
                    if (rand2 <= .5d)
                        value = 1;
                    else
                        value = 3;
                }

                playersMoves.Add(value); 
            }

            return playersMoves;
        }
    }
}
