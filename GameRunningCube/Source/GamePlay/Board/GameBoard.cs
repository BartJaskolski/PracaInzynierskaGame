using System.Collections.Generic;
using System.Linq;
using GameRunningCube.DbContext;
using GameRunningCube.Source.GamePlay;
using GameRunningCube.Source.GamePlay.Board;
using GameRunningCube.Source.Helpers;
using Microsoft.Xna.Framework;

namespace GameRunningCube
{
    public class GameBoard
    {
        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; }
        public ScoreSprite ScoreSprite { get; set; }

        public GameBoard()
        {
            SetDefaultValues();
        }

        public virtual void Draw()
        {
            Player.Draw();
            ScoreSprite.Draw();

            foreach (var enemy in Enemies)
                enemy.Draw();
        }

        public virtual void Update()
        {
            if (GlobalVariables.KeyboardController.GetPress("R"))
                Restart();

            Player.Update();
            ScoreSprite.Update();

            foreach (var enemy in Enemies)
                enemy.Update();
        }

        private void Restart()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            Player = new Player(new Vector2(300, 600), new Vector2(30, 30), "2D\\Player");
            ScoreSprite = new ScoreSprite(Player);
            Enemies = GetEnemiesFromDB();
            //Enemies = new List<Enemy>();
            //for (int i = 0; i < 10; i++)
            //{
            //    Enemies.Add(GlobalVariables.ObjectGenerator.GenerateRandomObject<Enemy>("2D\\Enemy"));
            //}
        }

        private List<Enemy> GetEnemiesFromDB()
        {
            List<EnemyDB> enemyData = new List<EnemyDB>();
            DbContextRunningCube dbContext = new DbContextRunningCube();
            enemyData = dbContext.EnemiesData.ToList();
            
            return MapDbToObj(enemyData);
        }

        private List<Enemy> MapDbToObj(List<EnemyDB> enemyData)
        {
            List<Enemy> enemies = new List<Enemy>();
            foreach (var enemyDb in enemyData)
            {
                enemies.Add(GlobalVariables.ObjectGenerator.ConvertDbToObj<Enemy>(enemyDb));
            }

            return enemies;
        }
    }
}
