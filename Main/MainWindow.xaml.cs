using System;
using System.Windows;
using GameRunningCube;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GameEngine;
using Main.Views;

namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameSettings GameSettings { get; set; }
        public ConfigurationRepository configurationRepository { get; set; }
        public MainWindow()
        {
            GlobalVariables.Random = new Random(10);
            InitializeComponent();
            GameSettings = new GameSettings();
            configurationRepository = new ConfigurationRepository();
            GetConfiguration();
        }

        private void GetConfiguration()
        {
            ParametersDB parameters = configurationRepository.GetParameters();
            GameSettings.AmountOfPopulation = parameters.AmountOfPopulation;
            GameSettings.SzybkoscGry = parameters.SpeedOfAGame;
            GameSettings.MuationPercent = parameters.MutationPercent;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            GameSettings.Tryb = GameMode.Genration;
            using (var game = new MainGame(GameSettings))
                game.Run();
        }

        private void Configure_Click(object sender, RoutedEventArgs e)
        {
            var configurationWindow = new ConfigurationWindow(GameSettings);
            configurationWindow.ShowDialog();

            GameSettings = configurationWindow.GameSetting;
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            GameSettings.Tryb = GameMode.Test;
            using (var game = new MainGame(GameSettings))
                game.Run();
        }
    }
}
