using System.ComponentModel.DataAnnotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRunningCube.Source.GamePlay.Board
{
    public class EnemyDB
    {
        [Key]
        public int IdObject { get; set; }
        public int CorrelationID { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public EnemyDB(int correlation, int height, int width, int posX, int posY)
        {
            CorrelationID = correlation;
            Height = height;
            Width = width;
            PosX = posX;
            PosY = posY;
        }

        public EnemyDB()
        {
            
        }

    }

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
