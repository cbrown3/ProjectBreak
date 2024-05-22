using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum StateType
{
    None,
    NormalAttack,
    SpecialAttack,
    Idle,
    Run,
    Guard,
    Grab,
    Throw,
    Thrown,
    Pushback,
    Dash,
    HitStun,
    BlockStun,
    RegularParry,
    NormalParry,
    SpecialParry,
    GrabParry,
    SoftKnockdown,
    HardKnockdown
}

abstract public class IState<T>
{
    //public StateType stateType;
    abstract public void Enter(T entity);
    abstract public void Continue(T entity);
    //abstract public void Continue(T entity, InputAction.CallbackContext context);
    abstract public void Exit(T entity);
}


public class StateMachine<T>
{
    T Owner;
    IState<T> CurrentState;
    IState<T> PreviousState;

    private bool isPaused;

    public void Awake()
    {
        CurrentState = null;
        PreviousState = null;
        isPaused = false;
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
        if(CurrentState != null && !isPaused)
        {
            CurrentState.Continue(Owner);
        }
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Play()
    {
        isPaused = false;
    }

    public IState<T> GetCurrentState()
    {
        return CurrentState;
    }
}
