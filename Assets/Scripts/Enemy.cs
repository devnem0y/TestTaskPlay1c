using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Rigidbody2D _rb;
    
    public int AttackDamage { get; private set; }
    
    private Action _callback;
    private int _health;

    public void Init(ConfigEnemy config, Action callback)
    {
        _callback = callback;
        _health = config.Health;
        
        AttackDamage = 1; //TODO: по тз захардкожено на 1. Но можно сделать, чтобы тянуть из конфига.
        
        var movementSpeed = Random.Range(config.MovementSpeedMin, config.MovementSpeedMax);
        _rb.AddForce(Vector2.down * movementSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Bullet")) return;
        
        var bullet = other.gameObject.GetComponent<IBullet>();
        _health -= bullet.Hit;

        if (_health > 0) return;
        
        _callback?.Invoke();
        Destroy(gameObject);
    }
}