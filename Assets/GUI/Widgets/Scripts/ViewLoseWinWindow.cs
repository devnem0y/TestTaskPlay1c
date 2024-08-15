using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UralHedgehog;
using UralHedgehog.UI;

public class ViewLoseWinWindow : Widget<IEmptyWidget>
{
    [SerializeField] private TMP_Text _lblTitle;
    [SerializeField] private Button _btnRestart;

    protected override void Awake()
    {
        base.Awake();
        
        _lblTitle.text = Game.Instance.GameState == GameState.VICTORY ? "You Win!" : "You Lose!";
        
        _btnRestart.onClick.AddListener(() =>
        {
            Game.Instance.ChangeState(GameState.PLAY);
            Hide();
        });
    }
}