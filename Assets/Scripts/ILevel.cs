using System;

public interface ILevel
{
    public int CountEnemies { get; }
    
    public event Action EnemyCountChanged;
}