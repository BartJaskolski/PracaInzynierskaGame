using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRunningCube.Source.GamePlay
{
    public class Player : Objects2D
    {
        public int Score { get; set; } = 0;
        public bool IsColidedX { get; set; }
        public bool IsColidedY { get; set; }
        public int MovesCount { get; set; }
        public Player(Vector2 position, Vector2 dimention, string path) : base(position, dimention, path)
        {
        }

        public override void Update()
        {
            MovePlayer();

            base.Update();
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
                GlobalVariables.SpriteBatch.Draw(Texture2D, new Rectangle(Location, Size), null, Color.Green, 0.0f, new Vector2(Texture2D.Bounds.Width / 2, Texture2D.Bounds.Height / 2), new SpriteEffects(), 0);
        }
    }
}
