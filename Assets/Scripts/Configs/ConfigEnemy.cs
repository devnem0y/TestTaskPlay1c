using UnityEngine;

[CreateAssetMenu(fileName = "ConfigEnemy", menuName = "Configs/Enemy", order = 2)]
public class ConfigEnemy : ScriptableObject
{
    [SerializeField, Min(0f)] private float _movementSpeedMin;
    public float MovementSpeedMin => _movementSpeedMin;
    
    [SerializeField, Min(0f)] private float _movementSpeedMax;
    public float MovementSpeedMax => _movementSpeedMax;
    
    [SerializeField] private int _health;
    public int Health => _health;

    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;
    
    [SerializeField] private Color _color;
    public Color Color => _color;
}