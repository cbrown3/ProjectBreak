using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackState : IState<CharController>
{
    public SpecialAttackState()
    {
        stateType = StateType.SpecialAttack;
    }
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
