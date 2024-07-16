using UnityEngine;

[CreateAssetMenu(fileName = "ConfigPlayer", menuName = "Configs/Player", order = 1)]
public class ConfigPlayer : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;
}