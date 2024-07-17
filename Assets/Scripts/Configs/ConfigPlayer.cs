using UnityEngine;

[CreateAssetMenu(fileName = "ConfigPlayer", menuName = "Configs/Player", order = 1)]
public class ConfigPlayer : ScriptableObject
{
    [SerializeField, Min(0f)] private float _bulletSpeed;
    public float BulletSpeed => _bulletSpeed;
    
    [SerializeField] private int _bulletDamage;
    public int BulletDamage => _bulletDamage;
    
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;
}