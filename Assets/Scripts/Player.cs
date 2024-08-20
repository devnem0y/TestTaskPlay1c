using System;
using R3;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _radius;
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _shootPoint;
    
    private ReactiveProperty<int> _health;
    public Observable<int> Health => _health;
    
    public int Damage { get; private set; }
    public event Action Death;

    private ConfigPlayer _configPlayer;
    private Vector2 _movement;
    private float _nextShootTime;

    public void Init(ConfigPlayer config)
    {
        _configPlayer = config;
        
        _health = new ReactiveProperty<int>(_configPlayer.Health);
        Damage = _configPlayer.BulletDamage;
        _nextShootTime = 0.1f;
    }
    
    public void OnTakeDamage(int damage)
    {
        if (_health.Value >= damage) _health.Value -= damage;
        else _health.Value = 0;
        
        if (_health.Value <= 0) Death?.Invoke();
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        
        TargetDetection();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
    
    private void Shoot()
    {
        _nextShootTime -= Time.deltaTime;
        
        if (!(_nextShootTime <= 0)) return;
        
        var bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
        bullet.Init(_shootPoint.up, _configPlayer.BulletSpeed, _configPlayer.BulletDamage);
        _nextShootTime = _configPlayer.ShootRate;
    }

    private void TargetDetection()
    {
        var hitColliders = Physics2D.OverlapCircleAll(_radius.position, _configPlayer.RadiusDefeat);

        foreach (var hc in hitColliders)
        {
            if (!hc.CompareTag("Enemy"))
            {
                _turret.rotation = Quaternion.Euler(0, 0, 0);
                continue;
            }
            
            var enemy = hc.transform;
            var direction = enemy.position - _turret.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _turret.rotation = Quaternion.Euler(0, 0, angle);
            Shoot();
            break;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (!EditorApplication.isPlaying) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_radius.position, _configPlayer.RadiusDefeat);
    }
}