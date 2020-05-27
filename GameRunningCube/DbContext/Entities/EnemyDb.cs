using System.ComponentModel.DataAnnotations;

namespace GameRunningCube.DbContext.Entities
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
}
