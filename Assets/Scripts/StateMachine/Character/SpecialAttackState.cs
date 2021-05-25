using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackState : IState<CharController>
{
    static readonly SpecialAttackState instance =
       new SpecialAttackState();

    public static SpecialAttackState Instance
    {
        get
        {
            return instance;
        }
    }

    static SpecialAttackState() { }

    private SpecialAttackState() { }

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
