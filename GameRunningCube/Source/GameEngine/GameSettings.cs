namespace GameRunningCube.Source.GameEngine
{
    public class GameSettings
    {
        public double SzybkoscGry { get; set; }
        public GameMode Tryb { get; set; }
        public int PreferredBackBufferWidth { get; set; } = 600;
        public double MuationPercent { get; set; }
        public double AmountOfPopulation { get; set; }
    }

    public enum GameMode
    {
        Test,
        Genration
    }

}
