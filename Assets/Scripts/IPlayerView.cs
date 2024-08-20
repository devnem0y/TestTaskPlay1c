using R3;

public interface IPlayerView
{
    public Observable<int> Health { get; }
    public int Damage{ get; }
}