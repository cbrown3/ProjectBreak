using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharController : MonoBehaviour
{
    public float groundSpeed = 12;
    public float dashSpeed = 25;
    public float jumpSpeed = 12;

    public enum State
    {
        Stop,
        AirDash,
        Walk,
        Run,
        Crouch,
        Jump,
        Fall
    }

    public static State currentState;

    [SerializeField]
    private State stateSerializationHelper;

    private Animator animator;
    private Rigidbody2D rigid;
    private int animTimer = 10;

    private const string aStopAnim = "Stop";
    private const string aAirDashAnim = "AirDash";
    private const string aWalkAnim = "Walk";
    private const string aRunAnim = "Run";
    private const string aCrouchAnim = "Crouch";
    private const string aJumpAnim = "Jump";
    private const string aFallAnim = "Fall";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        stateSerializationHelper = currentState;
    }

    private void FixedUpdate()
    {
        ContinueState();
    }

    private void EnterState(State stateEntered)
    {
        switch(stateEntered)
        {
            case State.Stop:
                break;

            case State.AirDash:
                break;

            case State.Walk:
                break;

            case State.Run:
                break;

            case State.Crouch:
                break;

            case State.Jump:
                break;

            case State.Fall:
                break;

            default:
                break;
        }
    }

    private void ContinueState()
    {
        switch (currentState)
        {
            case State.Stop:
                break;

            case State.AirDash:
                break;

            case State.Walk:
                break;

            case State.Run:
                break;

            case State.Crouch:
                break;

            case State.Jump:
                break;

            case State.Fall:
                break;

            default:
                break;
        }
    }
}
