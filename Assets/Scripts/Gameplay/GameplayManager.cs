using FightLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject CameraManager;

    public static GameplayManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator HitStop(float time)
    {
        CharManager.player1.stateMachine.Pause();
        CharManager.player2.stateMachine.Pause();
        CharManager.player1.animator.speed = 0;
        CharManager.player2.animator.speed = 0;

        yield return new WaitForSeconds(time);
        
        CharManager.player1.stateMachine.Play();
        CharManager.player2.stateMachine.Play();
        CharManager.player1.animator.speed = 1;
        CharManager.player2.animator.speed = 1;
    }
}
