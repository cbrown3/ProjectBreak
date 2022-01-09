using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public bool isPlayer1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);

        if (collision.name == "Character Collider Blocker")
        {
            if(isPlayer1 && collision != CharManager.player1.charBlockerCollider)
            {
                CharManager.player2.EnterState("HitStun");
            }
            else if(!isPlayer1 && collision != CharManager.player2.charBlockerCollider)
            {
                CharManager.player1.EnterState("HitStun");
            }
        }
    }
}
