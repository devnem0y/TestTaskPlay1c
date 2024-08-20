using System;
using R3;
using TMPro;
using UnityEngine;
using UralHedgehog.UI;

public class ViewTopPanel : Widget<ITopPanel>
{
    [SerializeField] private TMP_Text _lblPlayerHealth;
    [SerializeField] private TMP_Text _lblPlayerDamage;
    [SerializeField] private TMP_Text _lblCountEnemies;
    
    private IDisposable _playerHealth;
    private IDisposable _countEnemies;
        
    public override void Init(ITopPanel model)
    {
        base.Init(model);

        _lblPlayerDamage.text = $"<sprite=3> {Model.Player.Damage}";
        
        _playerHealth = Model.Player.Health.Subscribe(value => { _lblPlayerHealth.text = $"<sprite=2> {value}"; });
        _countEnemies = Model.Level.CountEnemies.Subscribe(value => { _lblCountEnemies.text = $"<sprite=1> {value}"; });
    }

    private void OnDestroy()
    {
        _playerHealth.Dispose();
        _countEnemies.Dispose();
    }
}