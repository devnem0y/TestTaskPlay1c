using UnityEngine;

[CreateAssetMenu(fileName = "ConfigLevel", menuName = "Configs/Level", order = 3)]
public class ConfigLevel : ScriptableObject
{
    [SerializeField] private int _countEnemies;
}