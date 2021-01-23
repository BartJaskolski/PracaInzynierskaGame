using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GameRunningCube;
using GameRunningCube.DbContext;
using GameRunningCube.DbContext.Entities;
using GameRunningCube.Source.GameEngine;
using HelpersGRC;
using Main.ViewModel;

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
            this.DataContext = new ConfigurationViewModel();
        }

        public List<EnemyDB> Enemies { get; set; }
        public GameSettings GameSetting { get; set; }
        public RandomNumber NumberGenerator;

        private void btn_generuj_Click(object sender, RoutedEventArgs e)
        {
            Enemies = new List<EnemyDB>();
            bool[,] field = new bool[30,60];
            for (int i = -90; i < 28; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int generatedY = i;
                    int genratedX = GlobalVariables.Random.Next(0, 30);
                    Enemies.Add(new EnemyDB(1, 19, 19, genratedX * 20, generatedY * 20));

                }
            }

            //for (int i = 0; i < 40; i++)
            //{
            //    int generatedX = GlobalVariables.Random.Next(0, 30);
            //    int genratedY = GlobalVariables.Random.Next(-30, 30);
            //    field[generatedX,genratedY] = true;
            //    Enemies.Add(new EnemyDB(1,19,19, generatedX * 20, genratedY * 20));
            //}

            using (var db = new DbContextRunningCube())
            {
                db.EnemiesData.AddRange(Enemies);
                db.SaveChanges();
            }
        }

        //private void btn_gen_pop_Click(object sender, RoutedEventArgs e)
        //{
        //    using (var db = new DbContextRunningCube())
        //    {
        //        if (!db.PopulationData.Any())
        //        {
        //            db.PopulationData.AddRange(
        //                MapPlayersToPopulation(
        //                    new PopulationGenerator().GeneratedPopulation));
        //            db.SaveChanges();

        //            lb_pop_gen.Content = "Generated!";
        //        }
        //        else
        //        {
        //            lb_pop_gen.Content = "Population 0 is already generated!";
        //        }
        //    }
            
        //    //lb_pop_gen.Content += "Generated members: "+ GeneratedPopulation.Count()+"/r";
        //}

   

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new DbContextRunningCube())
            {
                if (db.EnemiesData.Any())
                {
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE [EnemyDBs]");
                    db.SaveChanges();
                }
            }
        }
    }
}
