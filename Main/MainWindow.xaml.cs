﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameRunningCube;
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
        public MainWindow()
        {
            GlobalVariables.Random = new Random(10);
            InitializeComponent();
            GameSettings = new GameSettings();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            using (var game = new MainGame(GameSettings))
                game.Run();
        }

        private void Configure_Click(object sender, RoutedEventArgs e)
        {
            var configurationWindow = new ConfigurationWindow();
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
