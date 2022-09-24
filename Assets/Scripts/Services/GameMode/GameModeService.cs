namespace Services.GameMode
{
    public class GameModeService : IGameModeService
    {
        public GameMode GameMode { get; private set; }
        
        public void SetGameMode(GameMode gameMode)
        {
            GameMode = gameMode;
        }
    }
}