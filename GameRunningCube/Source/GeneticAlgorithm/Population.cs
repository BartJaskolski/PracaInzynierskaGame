using System;
using System.Collections.Generic;
using GameRunningCube.DbContext.Entities;

namespace GameRunningCube.Source.GeneticAlgorithm
{
    public class Population
    {
        public int IdObject { get; set; }
        public List<int> AiMoves { get; set; }
        public int Score { get; set; }
        public int GenerationNumber { get; set; }
        public bool AfterGame { get; set; }
        public int MovesCount { get; set; }
        public int Mutations { get; set; }


        public Population(int idObj, string aiMoves, int score, int generationNumber, int movesCount)
        {
            IdObject = idObj;
            AiMoves = AiMovesConverter(aiMoves);
            Score = score;
            GenerationNumber = generationNumber;
            AfterGame = false;
            MovesCount = movesCount;
        }

        private List<int> AiMovesConverter(string aimoves)
        {
            List<int> listaRuchow = new List<int>();
            for (int i = 0; i < aimoves.Length; i++)
            {
                string aIMove = aimoves.Substring(i, 1);

                listaRuchow.Add(Convert.ToInt32(aIMove));
            }

            return listaRuchow;
        }

        public string AiMovesToStrong(List<int> moves)
        {
            string aiMovesToString = "";
            foreach (var move in moves)
            {
                aiMovesToString += move.ToString();
            }

            return aiMovesToString;
        }

    }
}
