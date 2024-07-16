using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfigurationStorage", menuName = "Configs/Storage", order = 0)]
public class EnemyConfigurationStorage : ScriptableObject
{
    [SerializeField] private GameObject _defaultPrefab;
    public GameObject DefaultPrefab => _defaultPrefab;
    
    [SerializeField] private List<ConfigEnemy> _configEnemies;

    public ConfigEnemy GetRandomConfigEnemy()
    {
        if (_configEnemies.Count == 0) return null;
        
        var randomIndex = Random.Range(0, _configEnemies.Count - 1);
        return _configEnemies[randomIndex];
    }
}