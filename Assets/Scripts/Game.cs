using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set;}
    
    [SerializeField] private Level _level;
    
    public GameState GameState { get; private set; }

    private void Awake()
    {
        Instance = this;
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
                break;
            case GameState.PLAY:
                Debug.Log("<color=yellow>Play</color>");
                _level.Run();
                break;
            case GameState.VICTORY:
                Debug.Log("<color=yellow>Victory</color>");
                break;
            case GameState.DEFEAT:
                Debug.Log("<color=yellow>Defeat</color>");
                break;
                
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
