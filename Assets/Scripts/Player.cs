using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public int Health { get; private set; }
    
    public event Action TakeDamage;
    public event Action Death;

    public void Init(ConfigPlayer config)
    {
        
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
}