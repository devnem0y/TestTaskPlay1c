using System;
using UnityEngine;
using UralHedgehog.UI;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set;}
    
    [SerializeField] private Level _level;
    [SerializeField] private Shake _camera;
    
    public GameState GameState { get; private set; }
    public Shake Camera => _camera;
    
    private UIManager _uiManager;

    private void Awake()
    {
        Instance = this;
        
        _uiManager = new UIManager(FindObjectOfType<UIRoot>());
    }

    private void Start()
    {
        ChangeState(GameState.MAIN);
    }
    
    public void ChangeState(GameState state)
    {
        GameState = state;
        
        switch (GameState)
        {
            case GameState.MAIN:
                Debug.Log("<color=yellow>Main</color>");
                //ChangeState(GameState.PLAY);
                _uiManager.OpenViewMainPanel();
                break;
            case GameState.PLAY:
                Debug.Log("<color=yellow>Play</color>");
                _level.Run();
                _uiManager.OpenViewTopPanel(new TopPanel(_level));
                break;
            case GameState.VICTORY:
                Debug.Log("<color=yellow>Victory</color>");
                _uiManager.OpenViewLoseWinWindow();
                break;
            case GameState.DEFEAT:
                Debug.Log("<color=yellow>Defeat</color>");
                _uiManager.OpenViewLoseWinWindow();
                break;
                
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
