﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;
using GameRunningCube;
using GameRunningCube.DbContext;
using GameRunningCube.Source.GameEngine;
using GameRunningCube.Source.GamePlay.Board;

namespace Main.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            InitializeComponent();
        }

        public List<EnemyDB> Enemies { get; set; }
        public GameSettings GameSetting { get; set; }

        private void btn_generuj_Click(object sender, RoutedEventArgs e)
        {
            Enemies = new List<EnemyDB>();
            for (int i = 0; i < 10; i++)
            {
                Enemies.Add(new EnemyDB(1,30,30, (int)GlobalVariables.Random.Next(0, 600), (int)GlobalVariables.Random.Next(0, 200)));
            }

            using (var db = new DbContextRunningCube())
            {
                db.EnemiesData.AddRange(Enemies);
                db.SaveChanges();
            }
        }
    }
}