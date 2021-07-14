using System;
using UnityEngine;


/// <summary>
/// All game events are here.
/// I dont know if I will use it all. But its a good start...
/// 
/// 
/// This class has no use here. Maybe in the future.
/// </summary>
public class EventManager : Singleton<EventManager>
{

    public Action OnLevelWon;
    public void TriggerOnLevelWon()
    {
        OnLevelWon?.Invoke();
    }

}

public enum GameStateEnum
{
    GameStart,
    LevelStart,
    LevelBoot,
    InGame,
    Pause,
    EndGame
}

public static class GameState
{
    private static GameStateEnum gameState;

    public static Action<GameStateEnum> OnGameStateStart;
    public static void ChangeState(GameStateEnum newState)
    {
        gameState = newState;
        OnGameStateStart?.Invoke(gameState);
    }
    public static GameStateEnum CurrentState()
    {
        return gameState;
    }
}