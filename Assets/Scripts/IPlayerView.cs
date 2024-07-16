using System;

public interface IPlayerView
{
    public int Health { get; }
    
    public event Action TakeDamage;
}