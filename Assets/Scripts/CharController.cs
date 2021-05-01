using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharController : MonoBehaviour
{

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
    public float GroundSpeed { get => groundSpeed; set => groundSpeed = value; }

    [SerializeField]
    private State stateSerializationHelper;

    [SerializeField]
    private Vector2 velocitySerializationHelper;

    private CharacterControls charControls;

    private Animator animator;
    private Rigidbody2D rigid;
    private Collider2D collider;
    private int dashFrames;
    private bool isGrounded = false;
    private bool canDash = false;
    private bool canDJump = false;

    [SerializeField]
    private LayerMask ground;

    [SerializeField]
    private float groundSpeed = 12;

    [SerializeField]
    private float dashSpeed = 25;

    [SerializeField]
    private float jumpSpeed = 12;

    [SerializeField]
    private float moveInput;
    private float airDashInput;

    private const string aStopAnim = "Stop";
    private const string aAirDashAnim = "AirDash";
    private const string aWalkAnim = "Walk";
    private const string aRunAnim = "Run";
    private const string aCrouchAnim = "Crouch";
    private const string aJumpAnim = "Jump";
    private const string aFallAnim = "Fall";

    private void Awake()
    {
        charControls = new CharacterControls();
    }

    private void OnEnable()
    {
        charControls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        charControls.Character.Jump.performed += _ => EnterState(State.Jump);
        charControls.Character.AirDash.performed += _ => EnterState(State.AirDash);
        charControls.Character.Move.performed += _ => EnterState(State.Run);
    }

    // Update is called once per frame
    void Update()
    {
        stateSerializationHelper = currentState;
    }

    private void FixedUpdate()
    {
        ContinueState();

        /*
        //if(!isDashing)
        //{
            float movementInput = charControls.Character.Move.ReadValue<float>();

            Vector2 currentPos = transform.position;

            currentPos.x += movementInput * groundSpeed * Time.deltaTime;

            transform.position = currentPos;
        //}
        */
        velocitySerializationHelper = rigid.velocity;
    }

    public void EnterState(State stateEntered)
    {
        switch(stateEntered)
        {
            case State.Stop:
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                canDash = false;
                canDJump = false;

                currentState = stateEntered;
                break;

            case State.AirDash:
                if (!isGrounded && canDash)
                {
                    airDashInput = charControls.Character.AirDash.ReadValue<float>();
                    dashFrames = 12;

                    rigid.velocity = Vector2.zero;

                    canDash = false;
                    canDJump = false;
                    currentState = stateEntered;
                }
                break;

            case State.Walk:
            case State.Run:
                if(isGrounded)
                {
                    moveInput = charControls.Character.Move.ReadValue<float>();
                    canDash = false;
                    canDJump = false;

                    rigid.AddForce(new Vector2(groundSpeed * moveInput, 0), ForceMode2D.Impulse);

                    currentState = stateEntered;
                }
                break;

            case State.Crouch:
                canDash = false;
                canDJump = false;
                break;

            case State.Jump:
                if(isGrounded || canDJump)
                {
                    canDJump = true ? isGrounded : !isGrounded;
                    isGrounded = false;
                    canDash = canDJump;

                    rigid.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

                    currentState = stateEntered;
                }
                break;

            case State.Fall:
                currentState = stateEntered;
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
                if (Mathf.Round(rigid.velocity.x) != 0)
                {
                    EnterState(State.Run);
                }
                break;

            case State.AirDash:
                if(dashFrames > 10)
                {
                    rigid.velocity = Vector2.zero;
                }
                else if(dashFrames < 1)
                {
                    rigid.velocity = Vector2.zero;

                    EnterState(State.Fall);
                }
                else
                {
                    //rigid.AddForce(new Vector2(dashSpeed * airDashInput, 0), ForceMode2D.Impulse);
                    rigid.velocity = new Vector2(dashSpeed * airDashInput, 0);
                }

                dashFrames--;
                break;

            case State.Walk:
            case State.Run:
                moveInput = charControls.Character.Move.ReadValue<float>();

                rigid.AddForce(new Vector2(groundSpeed * moveInput, 0), ForceMode2D.Impulse);

                if (rigid.velocity.x > groundSpeed)
                {
                    rigid.velocity = new Vector2(groundSpeed, rigid.velocity.y);
                }
                else if (rigid.velocity.x < -groundSpeed)
                {
                    rigid.velocity = new Vector2(-groundSpeed, rigid.velocity.y);
                }
                else
                {
                    EnterState(State.Stop);
                }
                break;

            case State.Crouch:
                break;

            case State.Jump:
                if (Mathf.Round(rigid.velocity.y) < 0)
                {
                    EnterState(State.Fall);
                    break;
                }

                CalculateMovement();

                break;

            case State.Fall:
                if (Mathf.Round(rigid.velocity.y) == 0)
                {
                    if(Mathf.Round(rigid.velocity.x) != 0)
                    {
                        EnterState(State.Run);
                    }
                    else
                    {
                        EnterState(State.Stop);
                    }
                }

                CalculateMovement();

                break;

            default:
                break;
        }
    }

    private void OnDisable()
    {
        charControls.Disable();
    }

    /*
     private bool IsGrounded()
    {
        Vector2 topLeft = transform.position;
        topLeft.x -= collider.bounds.extents.x;
        topLeft.y += collider.bounds.extents.y;

        Vector2 bottomRight = transform.position;
        bottomRight.x += collider.bounds.extents.x;
        bottomRight.y -= collider.bounds.extents.y;

        return Physics2D.OverlapArea(topLeft, bottomRight, ground); ;
    }
    */

    private void CalculateMovement()
    {
        moveInput = charControls.Character.Move.ReadValue<float>();

        rigid.AddForce(new Vector2(groundSpeed * moveInput, 0), ForceMode2D.Impulse);

        if (rigid.velocity.x > groundSpeed)
        {
            rigid.velocity = new Vector2(groundSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < -groundSpeed)
        {
            rigid.velocity = new Vector2(-groundSpeed, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(groundSpeed * moveInput, rigid.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MainCamera")
        {
            Debug.Log("leaving cam");
            collision.transform.Translate(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }
}
