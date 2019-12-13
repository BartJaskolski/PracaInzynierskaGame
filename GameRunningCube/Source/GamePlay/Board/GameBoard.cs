using System.Collections.Generic;
using GameRunningCube.Source.GamePlay;
using GameRunningCube.Source.GamePlay.Board;
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
            Enemies = new List<Enemy>();
            Enemies.Add(GlobalVariables.ObjectGenerator.GenerateRandomObject<Enemy>("2D\\Enemy"));
        }
    }
}
