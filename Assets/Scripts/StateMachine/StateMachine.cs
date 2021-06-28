using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class IState<T>
{
    abstract public void Enter(T entity);
    abstract public void Continue(T entity);
    abstract public void Exit(T entity);
}


public class StateMachine<T>
{
    T Owner;
    IState<T> CurrentState;
    IState<T> PreviousState;

    public void Awake()
    {
        CurrentState = null;
        PreviousState = null;
    }

    public void Configure(T owner, IState<T> initialState)
    {
        Owner = owner;
        EnterState(initialState);
    }

    public void EnterState(IState<T> stateEntered)
    {
        PreviousState = CurrentState;

        if(CurrentState != null)
        {
            CurrentState.Exit(Owner);
        }

        CurrentState = stateEntered;

        if (CurrentState != null)
        {
            CurrentState.Enter(Owner);
        }
    }

    public void RevertToPreviousState()
    {
        if(PreviousState != null)
        {
            EnterState(PreviousState);
        }
    }

    public void Update()
    {
        if(CurrentState != null)
        {
            CurrentState.Continue(Owner);
        }
    }

    public IState<T> GetCurrentState()
    {
        return CurrentState;
    }
}