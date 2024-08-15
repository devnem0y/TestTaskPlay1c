using UnityEngine;

[CreateAssetMenu(fileName = "ConfigPlayer", menuName = "Configs/Player", order = 1)]
public class ConfigPlayer : ScriptableObject
{
    [SerializeField] private int _health;
    public int Health => _health;
    
    [SerializeField, Min(0f)] private float _bulletSpeed;
    public float BulletSpeed => _bulletSpeed;
    
    [SerializeField, Min(0f)] private float _radiusDefeat;
    public float RadiusDefeat => _radiusDefeat;
    
    [SerializeField, Min(0f)] private float _shootRate;
    public float ShootRate => _shootRate;
    
    [SerializeField] private int _bulletDamage;
    public int BulletDamage => _bulletDamage;
    
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;
}