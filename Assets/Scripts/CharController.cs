using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharController : MonoBehaviour
{
    StateMachine<CharController> stateMachine;

    [SerializeField]
    private string stateSerializationHelper = "";

    [SerializeField]
    private Vector2 velocitySerializationHelper = Vector2.zero;

    private CharacterControls charControls;

    public PlayerInput playerInput;

    //public InputActionTrace inputActionTrace;

    [NonSerialized]
    public Animator animator;

    [NonSerialized]
    public Rigidbody2D rigid;

    public bool interuptible,
    isGrounded,
    canDash,
    canAttack;

    [NonSerialized]
    public LayerMask ground;

    public float groundSpeed,
     dashSpeed,
     jumpHeight,
     aerialDrift,
     maxAerialSpeed;

    public int dashFrameLength,
    dashStartup,
    jumpSquatFrames,
    nNeutralGFrames,
    nSideGFrames,
    nUpGFrames,
    nDownGFrames,
    nNeutralAFrames,
    nSideAFrames,
    nUpAFrames,
    nDownAFrames;

    public float jumpInput, moveInput = 0;

    public Light2D glowLight;

    public GameObject colliders;

    public BoxCollider2D playerCollider;

    private Renderer renderer;

    private Shader normalShader, outlineShader;

    public Queue buffer;
    private double elapsedTime;

    public IdleState idleState;
    public AirDashState airDashState;
    public RunState runState;
    public JumpState jumpState;
    public FallState fallState;
    public NormalAttackState normalAttackState;
    public SpecialAttackState specialAttackState;
    public GuardState guardState;

    #region Animation Names

    [NonSerialized]
    public string aIdleAnim = "Base Layer.Advntr-Idle",
    aAirDashAnim = "Base Layer.Advntr-AirDash",
    aRunAnim = "Base Layer.Advntr-Run",
    aJumpAnim = "Base Layer.Advntr-Jump",
    aFallAnim = "Base Layer.Advntr-Fall",
    aGroundGuardAnim = "Base Layer.Advntr-GroundGuard",
    aAirGuardAnim = "Base Layer.Advntr-AirGuard",
    aNNeutralGroundAnim = "Base Layer.Advntr-NormalNeutralGround",
    aNSideGroundAnim = "Base Layer.Advntr-NormalSideGround",
    aNUpGroundAnim = "Base Layer.Advntr-NormalUpGround",
    aNDownGroundAnim = "Base Layer.Advntr-NormalDownGround",
    aNNeutralAerialAnim = "Base Layer.Advntr-NormalNeutralAerial",
    aNSideAerialAnim = "Base Layer.Advntr-NormalSideAerial",
    aNUpAerialAnim = "Base Layer.Advntr-NormalUpAerial",
    aNDownAerialAnim = "Base Layer.Advntr-NormalDownAerial",
    aSNeutralGroundAnim = "Base Layer.Advntr-SpecialNeutralGround",
    aSSideGroundAnim = "Base Layer.Advntr-SpecialSideGround",
    aSUpGroundAnim = "Base Layer.Advntr-SpecialUpGround",
    aSDownGroundAnim = "Base Layer.Advntr-SpecialDownGround",
    aSNeutralAerialAnim = "Base Layer.Advntr-SpecialNeutralAerial",
    aSSideAerialAnim = "Base Layer.Advntr-SpecialSideAerial",
    aSUpAerialAnim = "Base Layer.Advntr-SpecialUpAerial",
    aSDownAerialAnim = "Base Layer.Advntr-SpecialDownAerial";

    #endregion

    private void Awake()
    {
        buffer = new Queue(60);
        idleState = new IdleState();
        fallState = new FallState();
        runState = new RunState();
        jumpState = new JumpState();
        airDashState = new AirDashState();
        normalAttackState = new NormalAttackState();
        specialAttackState = new SpecialAttackState();
        guardState = new GuardState();

        playerInput = GetComponent<PlayerInput>();
        //inputActionTrace = new InputActionTrace();

        //charControls = new CharacterControls();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();

        stateMachine = new StateMachine<CharController>();
        stateMachine.Configure(this, idleState);

        maxAerialSpeed = aerialDrift + groundSpeed;

        //inputActionTrace.SubscribeTo(playerInput.currentActionMap);
    }

    private void OnEnable()
    {
        playerInput.ActivateInput();
        //charControls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        normalShader = Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default");
        outlineShader = Shader.Find("Shader Graphs/PlayerOutline");

        interuptible = true;

        playerInput.currentActionMap.actionTriggered += PlayerInput_onActionTriggered;

        //playerInput.SendMessage("Jump");
        //playerInput.SendMessage("AirDash");
        //playerInput.SendMessage("Movement");
        //playerInput.SendMessage("Attack", NormalAttackState.Instance);
        //playerInput.SendMessage("Guard");
        //charControls.Character.Jump.performed += _ => Jump();
        //charControls.Character.AirDash.performed += _ => AirDash();
        //charControls.Character.Move.performed += _ => Movement();
        //charControls.Character.HeavyNormal.started += _ => Attack(NormalAttackState.Instance);
        //charControls.Character.Guard.performed += _ => Guard();
    }

    // Update is called once per frame
    void Update()
    {
        stateSerializationHelper = stateMachine.GetCurrentState().ToString();

        moveInput =
            playerInput.
            actions.
            FindAction("Move").ReadValue<float>();

        Debug.Log(buffer.Count);

        if(buffer.Count > 0)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime > 0.05)
            {
                elapsedTime--;
                buffer.Dequeue();
            }
        }
        else
        {
            elapsedTime = 0;
        }
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

    #region Enter State 

    public void EnterState(IState<CharController> stateEntered)
    {
        stateMachine.EnterState(stateEntered);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        if(isGrounded && interuptible && moveInput != 0)
        {
            EnterState(runState);
        }
    }

    public void AirDash(InputAction.CallbackContext context)
    {
        if(stateMachine.GetCurrentState() != airDashState && interuptible)
        {
            EnterState(airDashState);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (interuptible)
        {
            EnterState(jumpState);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(canAttack && interuptible && context.started)
        {
            EnterState(normalAttackState);
        }
    }

    public void Guard(InputAction.CallbackContext context)
    {
        if (interuptible)
        {
            EnterState(guardState);
        }
    }

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
    {
        if(buffer.Count >= 60)
        {
            buffer.Dequeue();
        }

        if(obj.action.name != "DirectionalInput")
        {
            buffer.Enqueue(obj);
        }
    }

    #endregion

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

    public void SpecialAttackGlow()
    {
        glowLight.color = Color.cyan;
        glowLight.gameObject.SetActive(true);

        renderer.material.SetColor("_Color", Color.cyan);
        renderer.material.shader = outlineShader;
    }

    public void NormalAttackGlow()
    {
        glowLight.color = Color.red;
        glowLight.gameObject.SetActive(true);

        renderer.material.SetColor("_Color", Color.red);
        renderer.material.shader = outlineShader;
    }

    public void ResetGlow()
    {
        glowLight.gameObject.SetActive(false);

        renderer.sharedMaterial.shader = normalShader;
    }

    private void OnDisable()
    {
        /*inputActionTrace.Clear();
        inputActionTrace.UnsubscribeFromAll();
        inputActionTrace.Dispose();*/
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

        if(collision.gameObject.tag == "Character")
        {
            //collision.rigidbody.velocity = Vector2.zero;
            //rigid.velocity = Vector2.zero;
            //Physics2D.IgnoreCollision(playerCollider, collision.collider, true);
            //rigid.velocity = Vector3.ProjectOnPlane(rigid.velocity, collision.contacts[0].normal);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            //collision.rigidbody.;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
           //rigid.isKinematic = false;
        }
    }
}
