using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private Bullet _bullet;
    
    public int Health { get; private set; }
    
    public event Action TakeDamage;
    public event Action Death;

    private ConfigPlayer _configPlayer;

    public void Init(ConfigPlayer config)
    {
        _configPlayer = config;
    }
    
    public void OnTakeDamage(int damage)
    {
        if (Health >= damage)
        {
            Health -= damage;
        }
        else
        {
            Health = 0;
            Death?.Invoke();
        }
        
        TakeDamage?.Invoke();
    }

    public void Shoot()
    {
        var bullet = Instantiate(_bullet);
        bullet.Init(_configPlayer.BulletSpeed, _configPlayer.BulletDamage);
    }
}