using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpecialState : IState<CharController>
{
    static readonly LightSpecialState instance =
       new LightSpecialState();

    public static LightSpecialState Instance
    {
        get
        {
            return instance;
        }
    }

    static LightSpecialState() { }

    private LightSpecialState() { }

    public override void Enter(CharController c)
    {
        c.canAttack = false;
    }

    public override void Continue(CharController c)
    {
        
    }

    public override void Exit(CharController c)
    {

    }
}
