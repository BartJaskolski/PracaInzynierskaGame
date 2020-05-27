using System.ComponentModel.DataAnnotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRunningCube.Source.GamePlay
{

    public class Enemy : Objects2D
    {
        public Enemy(Vector2 position, Vector2 dimention, string path) : base(position, dimention, path)
        {
        }

        public Enemy() : base()
        {
        }

        public override void Update()
        {
            Position = new Vector2(Position.X, Position.Y+1);
        }

        public override void Draw()
        {
            if (Texture2D != null)
                GlobalVariables.SpriteBatch.Draw(Texture2D, 
                    new Rectangle(Location, Size), 
                    null, 
                    Color.Red,
                    0.0f, 
                    new Vector2(Texture2D.Bounds.Width / 2, Texture2D.Bounds.Height / 2), 
                    new SpriteEffects(), 
                    0);

        }

    }
}
