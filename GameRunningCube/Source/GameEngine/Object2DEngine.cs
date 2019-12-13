using System.Collections.Generic;
using GameRunningCube.Source.GamePlay;
using GameRunningCube.Source.GamePlay.Board;
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
            throw new System.NotImplementedException();
        }

        public bool IfObjectOutOfBoardGame(Objects2D obj)
        {
            if ((obj.Position.X - obj.Size.X/2) < 0) 
                obj.Position = new Vector2((0 + obj.Size.X / 2), obj.Position.Y);

            if ((obj.Position.X + obj.Size.X / 2) > 600)
                obj.Position = new Vector2((600 - obj.Size.X / 2), obj.Position.Y);

            if ((obj.Position.Y + obj.Size.Y / 2) > 600)
                obj.Position = new Vector2(obj.Position.X, (600 - obj.Size.Y / 2));

            return true;
        }

    }
}
