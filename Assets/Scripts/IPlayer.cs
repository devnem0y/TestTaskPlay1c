using System;

public interface IPlayer : IPlayerView
{
    public event Action Death;
    
    public void OnTakeDamage(int damage);
}