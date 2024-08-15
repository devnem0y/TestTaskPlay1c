using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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
    
    public void SpawnEnemy(Action callback)
    {
        var randomIndexPoint = Random.Range(0, _spawnEnemyPoints.Count);
        var randomConfigEnemy = _enemyConfigurationStorage.GetRandomConfigEnemy();
        var prefab = randomConfigEnemy.Prefab != null
            ? randomConfigEnemy.Prefab
            : _enemyConfigurationStorage.DefaultPrefab;
        var pos = _spawnEnemyPoints[randomIndexPoint].position;
        
        var enemyView = Object.Instantiate(prefab, pos, Quaternion.identity, _spawnEnemyPoints[randomIndexPoint]);
        var enemy = enemyView.GetComponent<Enemy>();
        enemy.Init(randomConfigEnemy, callback);
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
        if (_spawnPlayerPoint.childCount > 0)
        {
            _player = null;
            Object.Destroy(_spawnPlayerPoint.GetChild(0).gameObject);
            _isPlayerExist = false;
        }

        foreach (var enemyPoint in _spawnEnemyPoints.Where(enemyPoint => enemyPoint.childCount > 0))
        {
            for (var i = 0; i < enemyPoint.childCount; i++)
            {
                Object.Destroy(enemyPoint.GetChild(i).gameObject);
            }
        }
    }
}