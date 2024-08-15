using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private TMP_Text _lblHealth;
    
    public int AttackDamage { get; private set; }
    
    private Action _callback;
    private int _health;
    private float _moveSpeed;

    public void Init(ConfigEnemy config, Action callback)
    {
        _callback = callback;
        _health = config.Health;
        _lblHealth.text = $"{_health}";
        _moveSpeed = Random.Range(config.MovementSpeedMin, config.MovementSpeedMax);
        
        AttackDamage = 1; //TODO: по тз захардкожено на 1. Но можно сделать, чтобы тянуть из конфига.
    }
    
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + Vector2.down * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Bullet")) return;
        
        var bullet = other.gameObject.GetComponent<IBullet>();
        _health -= bullet.Hit;
        _lblHealth.text = _health > 0 ? $"{_health}" : "";

        if (_health > 0) return;
        
        _callback?.Invoke();
        Destroy(gameObject);
    }
}