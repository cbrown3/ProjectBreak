using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySpecialState : IState<CharController>
{
    static readonly HeavySpecialState instance =
       new HeavySpecialState();

    public static HeavySpecialState Instance
    {
        get
        {
            return instance;
        }
    }

    static HeavySpecialState() { }

    private HeavySpecialState() { }

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
