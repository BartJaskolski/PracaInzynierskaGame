using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GameEngine;
using GameRunningCube.Source.GamePlay;
using HelpersGRC;
using Main.Annotations;
using Main.Commands;
using Microsoft.Win32;

namespace Main.ViewModel
{
    public class ConfigurationViewModel : INotifyPropertyChanged
    {
        public ParametersRepository Configurationrepository { get; set; }
        public PopulationRepository PopulationRepository { get; set; }
        public GameSettings settings { get; set; }

        public ConfigurationViewModel(GameSettings gameSetting) : base()
        {
            PopulationRepository = new PopulationRepository();
            Configurationrepository = new ParametersRepository();
            settings = gameSetting;
            SpeedOfGame = gameSetting.SzybkoscGry.ToString();
            AmounOfPopulation = gameSetting.AmountOfPopulation.ToString();
            MutationPercent = gameSetting.MuationPercent.ToString();
        }

        public double SpeedOfameDobule
        {
            get
            {
                double res=0;
                if(!string.IsNullOrWhiteSpace(SpeedOfGame) && double.TryParse(SpeedOfGame, out res))
                    return res;
                return res;
            }
        }

        public int AmountOfPop
        {
            get
            {
                int res = 0;
                if (!string.IsNullOrWhiteSpace(AmounOfPopulation) && int.TryParse(AmounOfPopulation, out res))
                    return res;
                return res;
            }
        }

        public double MutationPrecentDouble
        {
            get
            {
                double res = 0;
                if (!string.IsNullOrWhiteSpace(MutationPercent) && double.TryParse(MutationPercent, out res))
                    return res;
                return res;
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
            if (SpeedOfameDobule == null || AmountOfPop == null || MutationPercent == null)
                return false;
            return !SpeedOfameDobule.Equals(0) && !AmountOfPop.Equals(0);
        }

        private void SaveParameters()
        {
            settings.AmountOfPopulation = AmountOfPop;
            settings.SzybkoscGry = SpeedOfameDobule;
            settings.MuationPercent = MutationPrecentDouble;


            Configurationrepository.SaveParameters(MutationPrecentDouble, SpeedOfameDobule, AmountOfPop);
        }

        #endregion


        public void GenerateStartPopulation()
        {
            LabelAfterGeneration = PopulationRepository.GenerateStartPopulation(MapPlayersToPopulation(
                            new PopulationGenerator(settings).GeneratedPopulation));
        }

        public bool CanGenerate()
        {
            bool result = true;
            if(settings.AmountOfPopulation == null || settings.AmountOfPopulation == 0)
            {
                LabelAfterGeneration = "Wielkośc populacji nie może być równa 0 ";
                return false;

            }
            if (!PopulationRepository.CanGenerateStartingPopulation())
            {
                LabelAfterGeneration = "Populacja jest już wygenerowana";
                return false;
            }
            return result;
        }

        private IEnumerable<PopulationDB> MapPlayersToPopulation(List<GameRunningCube.Source.GamePlay.Player> generatedPopulation)
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
