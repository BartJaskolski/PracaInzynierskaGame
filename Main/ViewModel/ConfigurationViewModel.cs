using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GamePlay;
using HelpersGRC;
using Main.Annotations;
using Main.Commands;
using Microsoft.Win32;

namespace Main.ViewModel
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        public double SpeedOfameDobule
        {
            get
            {
                return double.Parse(SpeedOfGame);
            }
        }

        public int AmountOfPop
        {
            get
            {
                return int.Parse(AmounOfPopulation);
            }
        }

        #region Binded properties

        private string _sppedOfGame;
        public string SpeedOfGame
        {
            get
            {
                return _sppedOfGame;
            }
            set
            {
                _sppedOfGame = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SpeedOfGame)));
            }
        }

        private string _labelAfterGeneration;
        public string LabelAfterGeneration
        {
            get
            {
                return _labelAfterGeneration;
            }
            set
            {
                _labelAfterGeneration = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(LabelAfterGeneration)));
            }
        }


        private string _amounOfPopulation;
        public string AmounOfPopulation
        {
            get
            {
                return _amounOfPopulation;
            }
            set
            {
                _amounOfPopulation = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(AmounOfPopulation)));
            }
        }

        private string _mutationPercent;

        public string MutationPercent 
        {
            get
            {
                return _mutationPercent;
            }
            set
            {
                _mutationPercent = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(MutationPercent)));
            }

        }

        #endregion

        #region Commands

        private ICommand _generateStatritngPopulation;

        public ICommand GenerateStatritngPopulation
        {
            get
            {
                return _generateStatritngPopulation ?? new CommandsHandler(GenerateStartPopulation, CanGenerate);
            }
        }

        private ICommand _saveParameters;
        public ICommand SaveParamaters
        {
            get
            {
                return _saveParameters ?? new CommandsHandler(SaveParameters, CanSaveParameters);
            }
        }

        private bool CanSaveParameters()
        {
            return !SpeedOfameDobule.Equals(0) && !AmountOfPop.Equals(0);
        }

        private void SaveParameters()
        {
            using (var db = new DbContextRunningCube())
            {
                db.Parameters.Add(new ParametersDB(double.Parse(MutationPercent), SpeedOfameDobule
                    , AmountOfPop));
                db.SaveChanges();
            }
        }

        #endregion


        public void GenerateStartPopulation()
        {
            using (var db = new DbContextRunningCube())
            {
                if (!db.PopulationData.Any())
                {
                    db.PopulationData.AddRange(
                        MapPlayersToPopulation(
                            new PopulationGenerator().GeneratedPopulation));
                    db.SaveChanges();

                    LabelAfterGeneration =  "Generated!";
                }
                else
                {
                    LabelAfterGeneration = "Population 0 is already generated!";
                }
            }
        }

        public bool CanGenerate()
        {
            return true;
        }

        private IEnumerable<PopulationDB> MapPlayersToPopulation(List<Player> generatedPopulation)
        {
            var puplationMapped = new List<PopulationDB>();

            foreach (var player in generatedPopulation)
            {
                var moves = string.Join(string.Empty, player.AiMoves);
                puplationMapped.Add(new PopulationDB(moves, 0));
            }

            return puplationMapped;
        }


        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
