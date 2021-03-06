﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRunningCube.Source.GamePlay
{
    public class Player : Objects2D
    {
        public int Score { get; set; }
        public bool IsColidedX { get; set; }
        public bool IsColidedY { get; set; }
        public int MovesCount { get; set; }
        public List<int> AiMoves { get; set; }
        public bool LoadedAiData { get; set; } = false;
        public int AiCounter { get; set; } = 0;
        public bool Lost { get; set; }

        private double _powerOfColl;
        public double PowerOfCollision
        {
            set
            {
                _powerOfColl = value;
                if(value != 0)
                    Score -= (int)(_powerOfColl * 1000);
            }
        }

        public Player(Vector2 position, Vector2 dimention, string path) : base(position, dimention, path)
        {
            AiCounter = 0;
            LoadedAiData = false;
        }

        public Player()
        {
        }

        public override void Update()
        {
            if (LoadedAiData)
                MoveAiPlayer();
            else
                MovePlayer();

            base.Update();
        }

        private void MoveAiPlayer()
        {
            int move = AiMoves[AiCounter];

            //prawo
            if (move == 3)
            {
                Position = new Vector2(Position.X + 20, Position.Y);
            }

            //lewo
            if (move == 1)
            {
                Position = new Vector2(Position.X - 20, Position.Y);
                // koszt przebytej drogi (ilosc ruchów do rezultatu (wysokosci Y ) )
            }

            AiCounter++;

            //Score -= 3;

            if (move == 2)
            {
                //Position = new Vector2(Position.X, Position.Y - 1);
                //MovesCount++;
            }
        }

        private void MovePlayer()
        {

            if (GlobalVariables.KeyboardController.GetPress("A"))
            {
                Position = new Vector2(Position.X - 1, Position.Y);
                Score -= 3;
                MovesCount++;
            }

            if (GlobalVariables.KeyboardController.GetPress("S"))
            {
                Position = new Vector2(Position.X, Position.Y + 1);
                Score -= 10;
                MovesCount++;
            }

            if (GlobalVariables.KeyboardController.GetPress("D"))
            {
                Position = new Vector2(Position.X + 1, Position.Y);
                Score -= 3;
                MovesCount++;
            }

            if (GlobalVariables.KeyboardController.GetPress("W"))
            {
                Score += 10;
                Position = new Vector2(Position.X, Position.Y - 1);
                MovesCount++;
            }
        }

        public override void Draw()
        {
            if (Texture2D != null)
                GlobalVariables.SpriteBatch.Draw(Texture2D, new Rectangle(Location.X, Location.Y, Size.X, Size.Y), null, Color.Green, 0.0f, new Vector2(Texture2D.Bounds.Width / 2, Texture2D.Bounds.Height / 2), new SpriteEffects(), 0);
        }
    }
}
