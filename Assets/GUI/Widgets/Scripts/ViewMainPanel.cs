using UnityEngine;
using UnityEngine.UI;
using UralHedgehog;
using UralHedgehog.UI;

public class ViewMainPanel : Widget<IEmptyWidget>
{
    [SerializeField] private Button _btnPlay;

    protected override void Awake()
    {
        base.Awake();
        
        _btnPlay.onClick.AddListener(() =>
        {
            Game.Instance.ChangeState(GameState.PLAY);
            Hide();
        });
    }
}