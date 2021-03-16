using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using GameRunningCube.Source.GeneticAlgorithm;

namespace GameRunningCube.DbContext.Entities
{
    public class PopulationDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdObject { get; set; }
        public string AiMoves { get; set; }
        public int Score { get; set; }
        public int GenerationNumber { get; set; }
        public int MovesCount { get; set; }
        public bool Trained { get; set; }

        public PopulationDB(string aiMoves, int generationNumber, int score = 0)
        {
            AiMoves = aiMoves;
            Score = score;
            GenerationNumber = generationNumber;
        }

        public PopulationDB(Population pop)
        {
            IdObject = pop.IdObject;
            AiMoves = string.Join(string.Empty, pop.AiMoves);
            Score = pop.Score;
            GenerationNumber = pop.GenerationNumber;
            MovesCount = 0;
        }

        public PopulationDB()
        {
        }

        public static int GetMaxIdObject()
        {
            using (var dbContext = new DbContextRunningCube())
            {
              return  dbContext.PopulationData.Max(x => x.IdObject);
            }
        }
    }
}
