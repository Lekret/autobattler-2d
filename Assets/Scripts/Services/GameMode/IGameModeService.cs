namespace Services.GameMode
{
    public interface IGameModeService
    {
        public GameMode GameMode { get; }
        void SetGameMode(GameMode gameMode);
    }
}