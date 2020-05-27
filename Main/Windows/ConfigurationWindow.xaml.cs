using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GameRunningCube;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GameEngine;
using HelpersGRC;

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
            NumberGenerator = new RandomNumber();
        }

        public List<EnemyDB> Enemies { get; set; }
        public GameSettings GameSetting { get; set; }
        public RandomNumber NumberGenerator;

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

        private void btn_gen_pop_Click(object sender, RoutedEventArgs e)
        {
            var population = new PopulationGenerator();
            var GeneratedPopulation = population.GeneratedPopulation;

            
            lb_pop_gen.Content += "Generated members: "+ GeneratedPopulation.Count()+"/r";

            lb_pop_gen.Content += "Generated!";
        }
    }
}
