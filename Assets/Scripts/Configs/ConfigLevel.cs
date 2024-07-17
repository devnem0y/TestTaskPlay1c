using UnityEngine;

[CreateAssetMenu(fileName = "ConfigLevel", menuName = "Configs/Level", order = 3)]
public class ConfigLevel : ScriptableObject
{
    [SerializeField, Min(0)] private int _countEnemiesMin;
    public int CountEnemiesMin => _countEnemiesMin;
    
    [SerializeField, Min(0)] private int _countEnemiesMax;
    public int CountEnemiesMax => _countEnemiesMax;
    
    [SerializeField, Min(0f)] private int _timeoutMin;
    public int TimeoutMin => _timeoutMin;
    
    [SerializeField, Min(0f)] private int _timeoutMax;
    public int TimeoutMax => _timeoutMax;
}