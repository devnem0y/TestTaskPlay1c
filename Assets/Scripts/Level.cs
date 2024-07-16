using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ConfigLevel _configLevel;
    
    [SerializeField] private  List<Transform> _spawnEnemyPoints;
    [SerializeField] private  Transform _spawnPlayerPoint;

    [SerializeField] private  EnemyConfigurationStorage _enemyConfigurationStorage;
    [SerializeField] private  ConfigPlayer _configPlayer;
    
    [SerializeField] private Trigger _trigger;
    
    private Spawner _spawner;
    private IPlayer _player;
    
    public IPlayerView Player => _player;

    private void Awake()
    {
        _spawner = new Spawner(_spawnEnemyPoints, _spawnPlayerPoint, _enemyConfigurationStorage, _configPlayer);
        
        _trigger.Enter += OnTriggerEntered;
    }

    public void Run()
    {
        if (_player != null) _player.Death -= OnPlayerDeath;
        _spawner.RemoveAll();

        _player = _spawner.SpawnPlayer();
        _player.Death += OnPlayerDeath;
    }

    private void OnTriggerEntered(int damage)
    {
        _player.OnTakeDamage(damage);
    }

    private void OnPlayerDeath()
    {
        Game.Instance.ChangeState(GameState.DEFEAT);
    }

    private void OnDestroy()
    {
        _trigger.Enter -= OnTriggerEntered;
    }
}