using System.ComponentModel.DataAnnotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRunningCube
{
    public class Objects2D : IObject2D
    {
        private Vector2 _dim;
        public Vector2 Dimention
        {
            get => _dim;
            set
            {
                _dim = value;
                Size = new Point((int)_dim.X, (int)_dim.Y);
            }
        }

        private Vector2 _pos;
        public Vector2 Position
        {
            get => _pos;
            set
            {
                _pos = value;
                Location = new Point((int)_pos.X, (int)_pos.Y);
            }
        }
        
        public Point Size { get; set; }
        public Point Location { get; set; }
        public Texture2D Texture2D { get; set; }
        public int Speed { get; set; } = 0;

        public Objects2D()
        {
            
        }
        public Objects2D(Vector2 position, Vector2 dimention, string path)
        {
            Position = position;
            Dimention = dimention;
            Texture2D = GlobalVariables.Content.Load<Texture2D>(path);
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            if(Texture2D!=null)
                GlobalVariables.SpriteBatch.Draw(Texture2D,new Rectangle(Location,Size),null, Color.Black, 0.0f, new Vector2(Texture2D.Bounds.Width/2,Texture2D.Bounds.Height/2),new SpriteEffects(), 0);
        }
    }

    public interface IObject2D
    {
        void Draw();
        void Update();
        
    }
}
