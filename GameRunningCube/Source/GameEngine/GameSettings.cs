namespace GameRunningCube.Source.GameEngine
{
    public class GameSettings
    {
        public double SzybkoscGry { get; set; }
        public GameMode Tryb { get; set; }
    }

    public enum GameMode
    {
        Test,
        Genration
    }

}
