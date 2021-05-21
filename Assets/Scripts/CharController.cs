using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharController : MonoBehaviour
{

    StateMachine<CharController> stateMachine;

    [SerializeField]
    private string stateSerializationHelper;

    [SerializeField]
    private Vector2 velocitySerializationHelper;

    public CharacterControls charControls;


    [NonSerialized]
    public Animator animator;

    [NonSerialized]
    public Rigidbody2D rigid;

    [NonSerialized]
    public bool isGrounded,
    canDash,
    canAttack;
    //,canDJump;

    [NonSerialized]
    public LayerMask ground;

    public float groundSpeed,
     dashSpeed,
     jumpHeight,
     aerialDrift;

    public int dashFrameLength,
    dashStartup,
    jumpSquatFrames,
    lNNeutralGFrames,
    lNSideGFrames,
    lNUpGFrames,
    lNDownGFrames,
    hNNeutralGFrames,
    hNSideGFrames,
    hNUpGFrames,
    hNDownGFrames;

    [NonSerialized]
    public float jumpInput, moveInput;

    /*
    public StopState stopState;
    public AirDashState airDashState;
    public RunState runState;
    public JumpState jumpState;
    public FallState fallState;
    */

    //[NonSerialized]
    public string aIdleAnim = "Base Layer.Advntr-Idle";
    public string aAirDashAnim = "Base Layer.Advntr-AirDash";
    public string aRunAnim = "Base Layer.Advntr-Run";
    public string aJumpAnim = "Base Layer.Advntr-Jump";
    public string aFallAnim = "Base Layer.Advntr-Fall";
    public string aLNNeutralGroundAnim = "Base Layer.Advntr-LightNormalNeutralGround";
    public string aLNSideGroundAnim = "Base Layer.Advntr-LightNormalSideGround";
    public string aLNUpGroundAnim = "Base Layer.Advntr-LightNormalUpGround";
    public string aLNDownGroundAnim = "Base Layer.Advntr-LightNormalDownGround";
    public string aHNNeutralGroundAnim = "Base Layer.Advntr-HeavyNormalNeutralGround";
    public string aHNSideGroundAnim = "Base Layer.Advntr-HeavyNormalSideGround";
    public string aHNUpGroundAnim = "Base Layer.Advntr-HeavyNormalUpGround";
    public string aHNDownGroundAnim = "Base Layer.Advntr-HeavyNormalDownGround";

    private void Awake()
    {
        charControls = new CharacterControls();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine<CharController>();
        stateMachine.Configure(this, IdleState.Instance);
    }

    private void OnEnable()
    {
        charControls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        charControls.Character.Jump.performed += _ => EnterState(JumpState.Instance);
        charControls.Character.AirDash.performed += _ => AirDash();
        charControls.Character.Move.performed += _ => Movement();
        charControls.Character.LightNormal.performed += _ => Attack(LightNormalState.Instance);
        charControls.Character.HeavyNormal.performed += _ => Attack(HeavyNormalState.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        stateSerializationHelper = stateMachine.GetCurrentState().ToString();
    }

    private void FixedUpdate()
    {
        //ContinueState(); 

        stateMachine.Update();

        velocitySerializationHelper = rigid.velocity;
    }

    /*
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
    */

    public void Movement()
    {
        if(isGrounded)
        {
            EnterState(RunState.Instance);
        }
    }

    public void AirDash()
    {
        if(stateMachine.GetCurrentState() != AirDashState.Instance)
        {
            EnterState(AirDashState.Instance);
        }
    }

    public void Attack(IState<CharController> state)
    {
        if(canAttack)
        {
            EnterState(state);
        }
    }

    public void EnterState(IState<CharController> stateEntered)
    {
        stateMachine.EnterState(stateEntered);
    }

    public void RevertToPreviousState()
    {
        stateMachine.RevertToPreviousState();
    }

    /*
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
        */

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
