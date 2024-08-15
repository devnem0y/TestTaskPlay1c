public class TopPanel : ITopPanel
{
    public IPlayerView Player { get; }
    public ILevel Level { get; }

    public TopPanel(Level level)
    {
        Player = level.Player;
        Level = level;
    }
}