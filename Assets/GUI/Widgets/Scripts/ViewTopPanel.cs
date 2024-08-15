using TMPro;
using UnityEngine;
using UralHedgehog.UI;

public class ViewTopPanel : Widget<ITopPanel>
{
    [SerializeField] private TMP_Text _lblPlayerHealth;
    [SerializeField] private TMP_Text _lblPlayerDamage;
    [SerializeField] private TMP_Text _lblCountEnemies;
        
    public override void Init(ITopPanel model)
    {
        base.Init(model);

        _lblPlayerDamage.text = $"<sprite=3> {Model.Player.Damage}";
        _lblPlayerHealth.text = $"<sprite=2> {Model.Player.Health}";
        Model.Player.TakeDamage += OnPlayerTakeDamage;
        
        _lblCountEnemies.text = $"<sprite=1> {Model.Level.CountEnemies}";
        Model.Level.EnemyCountChanged += OnEnemyCountChanged;
    }

    private void OnPlayerTakeDamage()
    {
        _lblPlayerHealth.text = $"<sprite=2> {Model.Player.Health}";
    }
    
    private void OnEnemyCountChanged()
    {
        _lblCountEnemies.text = $"<sprite=1> {Model.Level.CountEnemies}";
    }

    private void OnDestroy()
    {
        Model.Player.TakeDamage -= OnPlayerTakeDamage;
        Model.Level.EnemyCountChanged += OnEnemyCountChanged;
    }
}