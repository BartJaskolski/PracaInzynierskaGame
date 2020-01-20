using GameRunningCube.Source.GamePlay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameRunningCube
{
    public class ScoreSprite : Objects2D
    {
        public int score { get; set; } = 10000;
        public Player PlayerObj { get; set; }

        public ScoreSprite(Player player)
        {
            PlayerObj = player;
        }
        public override void Update()
        {
            score += PlayerObj.Score;
            score -= 1;
            PlayerObj.Score = 0;
            base.Update();
        }

        public override void Draw()
        {
            GlobalVariables.SpriteBatch.DrawString(GlobalVariables.SpriteFont, score.ToString(), new Vector2(10, 10), Color.Black);

            if(PlayerObj.IsColidedY)
                GlobalVariables.SpriteBatch.DrawString(GlobalVariables.SpriteFont, "kolizja z osia Y", new Vector2(100, 10), Color.Red);

        }

    }
}