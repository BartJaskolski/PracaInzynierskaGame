using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GameRunningCube;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GameEngine;
using GameRunningCube.Source.GamePlay;
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
            using (var db = new DbContextRunningCube())
            {
                if (!db.PopulationData.Any())
                {
                    db.PopulationData.AddRange(
                        MapPlayersToPopulation(
                            new PopulationGenerator().GeneratedPopulation));
                    db.SaveChanges();

                    lb_pop_gen.Content = "Generated!";
                }
                else
                {
                    lb_pop_gen.Content = "Population 0 is already generated!";
                }
            }
            
            //lb_pop_gen.Content += "Generated members: "+ GeneratedPopulation.Count()+"/r";
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
    }
}
