using System.Collections.Generic;
using GameRunningCube.Source.GamePlay;
using Microsoft.Xna.Framework;

namespace GameRunningCube.Source.GameEngine
{
    public class Object2DEngine
    {
        public void Update(GameBoard board)
        {
            IfObjectOutOfBoardGame(board.Player);
            IfObjectsColiding(board.Player, board.Enemies);
        }

        private bool IfObjectsColiding(Player boardPlayer, List<Enemy> boardEnemies)
        {
            foreach (var objects2D in boardEnemies)
            {
                if (IfTwoObjectsColiding(objects2D, boardPlayer))
                    return true;
            }
            return false;
        }

        private bool IfTwoObjectsColiding(Enemy objects2D, Player player)
        {
            return IfColidingOnX(objects2D,player) && IfColidingOnY(objects2D, player);
        }

        private bool IfColidingOnY(Enemy objects2D, Player player)
        {
            if (player.Location.Y >= objects2D.Location.Y
                && player.Location.Y <= (objects2D.Location.Y + objects2D.Size.Y))
                return player.IsColidedY = true;

            if ((player.Location.Y + player.Size.Y) >= objects2D.Location.Y
                && (player.Location.Y + player.Size.Y) <= (objects2D.Location.Y + objects2D.Size.Y))
                return player.IsColidedY = true;
            return player.IsColidedY = false;
        }

        private bool IfColidingOnX(Enemy objects2D, Player player)
        {
            if (player.Location.X >= objects2D.Location.X
                && player.Location.X <= (objects2D.Location.X + objects2D.Size.X))
                return player.IsColidedX = true;

            if ((player.Location.X + player.Size.X) > objects2D.Location.X
                && (player.Location.X + player.Size.X) <= (objects2D.Location.X + objects2D.Size.X))
                return player.IsColidedX = true;
            return player.IsColidedX = false;
        }

        public bool IfObjectOutOfBoardGame(Objects2D obj)
        {
            if ((obj.Position.X - obj.Size.X/2) < 0) 
                obj.Position = new Vector2((0 + obj.Size.X / 2), obj.Position.Y);

            if ((obj.Position.X + obj.Size.X / 2) > 600)
                obj.Position = new Vector2((600 - obj.Size.X / 2), obj.Position.Y);

            if ((obj.Position.Y + obj.Size.Y / 2) > 600)
                obj.Position = new Vector2(obj.Position.X, (600 - obj.Size.Y / 2));

            if ((obj.Position.Y - obj.Size.Y / 2) < 0)
                obj.Position = new Vector2(obj.Position.X, (0 + obj.Size.Y / 2));

            return true;
        }
    }
}