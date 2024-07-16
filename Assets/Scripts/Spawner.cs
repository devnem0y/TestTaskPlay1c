using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    private readonly List<Transform> _spawnEnemyPoints;
    private readonly Transform _spawnPlayerPoint;

    private readonly EnemyConfigurationStorage _enemyConfigurationStorage;
    private readonly ConfigPlayer _configPlayer;
    
    private bool _isPlayerExist;
    private Player _player;

    public Spawner(List<Transform> spawnEnemyPoints, Transform spawnPlayerPoint, EnemyConfigurationStorage enemyConfigurationStorage, ConfigPlayer configPlayer)
    {
        _spawnEnemyPoints = spawnEnemyPoints;
        _spawnPlayerPoint = spawnPlayerPoint;
        _enemyConfigurationStorage = enemyConfigurationStorage;
        _configPlayer = configPlayer;
    }
    
    public void SpawnEnemy()
    {
        var randomIndexPoint = Random.Range(0, _spawnEnemyPoints.Count - 1);
        var randomConfigEnemy = _enemyConfigurationStorage.GetRandomConfigEnemy();
        var prefab = randomConfigEnemy.Prefab != null
            ? randomConfigEnemy.Prefab
            : _enemyConfigurationStorage.DefaultPrefab;
        var pos = _spawnEnemyPoints[randomIndexPoint].position;
        
        var enemyView = Object.Instantiate(prefab, pos, Quaternion.identity, _spawnEnemyPoints[randomIndexPoint]);
    }

    public Player SpawnPlayer()
    {
        if (_isPlayerExist) return _player;
        
        var playerView = Object.Instantiate(_configPlayer.Prefab, _spawnPlayerPoint.position, Quaternion.identity, _spawnPlayerPoint);
        _player = playerView.GetComponent<Player>();
        _player.Init(_configPlayer);
        _isPlayerExist = true;

        return _player;
    }

    public void RemoveAll()
    {
        _player = null;
        Object.Destroy(_spawnPlayerPoint.GetChild(0).gameObject);

        foreach (var enemyPoint in _spawnEnemyPoints)
        {
            for (var i = 0; i < enemyPoint.childCount; i++)
            {
                Object.Destroy(enemyPoint.GetChild(i).gameObject);
            }
        }
    }
}