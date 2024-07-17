using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, ILevel
{
    [SerializeField] private ConfigLevel _configLevel;
    
    [SerializeField] private  List<Transform> _spawnEnemyPoints;
    [SerializeField] private  Transform _spawnPlayerPoint;

    [SerializeField] private  EnemyConfigurationStorage _enemyConfigurationStorage;
    [SerializeField] private  ConfigPlayer _configPlayer;
    
    [SerializeField] private Trigger _trigger;
    
    private Spawner _spawner;
    private IPlayer _player;
    
    public int CountEnemies { get; private set; }
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
        
        CountEnemies = Random.Range(_configLevel.CountEnemiesMin, _configLevel.CountEnemiesMax);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (Game.Instance.GameState == GameState.PLAY && CountEnemies > 0)
        {
            var timeoutSpawnEnemy = Random.Range(_configLevel.TimeoutMin, _configLevel.TimeoutMax);
            yield return new WaitForSeconds(timeoutSpawnEnemy);
            _spawner.SpawnEnemy(() =>
            {
                CountEnemies--;
                if (CountEnemies != 0) return;
                Game.Instance.ChangeState(GameState.VICTORY);
                StopAllCoroutines();
            });
        }
    }

    private void OnTriggerEntered(int damage)
    {
        _player.OnTakeDamage(damage);
    }

    private void OnPlayerDeath()
    {
        Game.Instance.ChangeState(GameState.DEFEAT);
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        _trigger.Enter -= OnTriggerEntered;
    }
}