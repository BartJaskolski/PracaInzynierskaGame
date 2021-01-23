namespace GameRunningCube.Source.GameEngine
{
    public class GameSettings
    {
        public double SzybkoscGry { get; set; }
        public GameMode Tryb { get; set; }
        public int PreferredBackBufferWidth { get; set; } = 600;
    }

    public enum GameMode
    {
        Test,
        Genration
    }

}
