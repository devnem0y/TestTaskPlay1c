using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

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
    
    private ReactiveProperty<int> _countEnemies;
    public Observable<int> CountEnemies => _countEnemies;
    
    public IPlayerView Player => _player;

    private void Awake()
    {
        _spawner = new Spawner(_spawnEnemyPoints, _spawnPlayerPoint, _enemyConfigurationStorage, _configPlayer);
        
        _trigger.Enter += OnTriggerEntered;
    }

    public void Run()
    {
        if (_player != null) _player.Death -= OnPlayerDeath;

        _player = _spawner.SpawnPlayer();
        _player.Death += OnPlayerDeath;
        
        var rndEnemies= Random.Range(_configLevel.CountEnemiesMin, _configLevel.CountEnemiesMax);
        _countEnemies = new ReactiveProperty<int>(rndEnemies);
        
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (Game.Instance.GameState == GameState.PLAY && _countEnemies.Value > 0)
        {
            var timeoutSpawnEnemy = Random.Range(_configLevel.TimeoutMin, _configLevel.TimeoutMax);
            yield return new WaitForSeconds(timeoutSpawnEnemy);
            _spawner.SpawnEnemy(() =>
            {
                _countEnemies.Value--;
                if (_countEnemies.Value != 0) return;
                _spawner.RemoveAll();
                Game.Instance.ChangeState(GameState.VICTORY);
                StopAllCoroutines();
            });
        }
    }

    private void OnTriggerEntered(int damage)
    {
        _player.OnTakeDamage(damage);
        Game.Instance.Camera.ShakeStart();
    }

    private void OnPlayerDeath()
    {
        _spawner.RemoveAll();
        Game.Instance.ChangeState(GameState.DEFEAT);
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        _trigger.Enter -= OnTriggerEntered;
    }
}