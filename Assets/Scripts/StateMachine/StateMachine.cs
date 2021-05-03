using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Continue();
    public void Exit();
}


public class StateMachine
{
    IState currentState;

    public void EnterState(IState stateEntered)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = stateEntered;

        currentState.Enter();
    }

    public void Update()
    {
        if(currentState != null)
        {
            currentState.Continue();
        }
    }
}
