using System.ComponentModel.DataAnnotations;

namespace GameRunningCube.DbContext.Entities
{
    public class PopulationDB
    {
        [Key]
        public int IdObject { get; set; }
        public string AiMoves { get; set; }
        public int Score { get; set; }
        public int GenerationNumber { get; set; }

        public PopulationDB(string aiMoves, int generationNumber, int score = 0)
        {
            AiMoves = aiMoves;
            Score = score;
            GenerationNumber = generationNumber;
        }
    }
}
