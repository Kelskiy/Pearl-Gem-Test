using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState currentState;

    public void ChangedGameState(GameState state)
    {
        if (currentState == state)
            return;

        currentState = state;
    }
}

public enum GameState
{ 
    WaitingForStart,
    Gameplay,
    GameOver
}
