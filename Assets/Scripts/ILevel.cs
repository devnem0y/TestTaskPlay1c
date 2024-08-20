using R3;

public interface ILevel
{
    public Observable<int> CountEnemies { get; }
    
    /*public int CountEnemies { get; }
    
    public event Action EnemyCountChanged;*/
}