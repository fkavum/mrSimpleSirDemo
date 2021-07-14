using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All inputs like keyboard, joystick etc. will be handled here. 
/// Player will only look horizontal and vertical float variables to decide where to go.
/// </summary>
public class InputManager : Singleton<InputManager>
{

    public VariableJoystick joystick;

    //[HideInInspector]
    public float horizontal;
    //[HideInInspector]
    public float vertical;

    private void Start()
    {
        GameState.OnGameStateStart += OnGameStateStart;
    }

    private void OnGameStateStart(GameStateEnum gameState)
    {
        if (GameStateEnum.InGame == gameState)
            joystick.Reset();

    }

    void Update()
    {
        if(GameState.CurrentState() != GameStateEnum.InGame)
        {
            horizontal = 0;
            vertical = 0;
            return;
        }

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        //INFO: Keyboard buttons override the joystick Inputs!
        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1;
        }


        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1;
        }

    }
}
