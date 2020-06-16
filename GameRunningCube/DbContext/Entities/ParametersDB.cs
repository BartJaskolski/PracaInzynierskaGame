using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRunningCube.DbContext.Entities
{
    public class ParametersDB
    {
        [Key]
        public int IdObject { get; set; }
        public double SpeedOfAGame { get; set; }

        public int AmountOfPopulation { get; set; }
        public double MutationPercent { get; set; }

        public ParametersDB(double mutation, double speed, int amount)
        {
            SpeedOfAGame = speed;
            AmountOfPopulation = amount;
            MutationPercent = mutation;
        }
    }
}
