using System;

public interface IPlayerView
{
    public int Health { get; }
    public int Damage{ get; }
    
    public event Action TakeDamage;
}