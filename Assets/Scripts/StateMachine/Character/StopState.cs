using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopState : MonoBehaviour, IState
{
    CharController owner;

    public StopState(CharController owner)
    {
        this.owner = owner;
    }

    public void Enter()
    {
        owner.rigid.velocity = new Vector2(0, owner.rigid.velocity.y);

        owner.canDash = owner.canDJump = false; 
    }

    public void Continue()
    {

    }

    public void Exit()
    {

    }
}
