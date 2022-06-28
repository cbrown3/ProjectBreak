using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public bool isPlayer1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        //Character Layer
        if (collision.gameObject.layer == 7)
        {
            if(isPlayer1 && collision != CharManager.player1.playerCollider)
            {
                CharManager.player2.EnterState("HitStun");
            }
            else if(!isPlayer1 && collision != CharManager.player2.playerCollider)
            {
                CharManager.player1.EnterState("HitStun");
            }
        }
    }
}
