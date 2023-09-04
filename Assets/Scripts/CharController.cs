using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharController : MonoBehaviour
{
    [NonSerialized]
    public StateMachine<CharController> stateMachine;

    [SerializeField]
    private string stateSerializationHelper = "";

    [SerializeField]
    private Vector2 velocitySerializationHelper = Vector2.zero;

    private CharacterControls charControls;

    public PlayerInput playerInput;

    public delegate void OnHealthChangedDelegate();
    public event OnHealthChangedDelegate OnHealthChanged;

    public delegate void OnStaminaChangedDelegate();
    public event OnStaminaChangedDelegate OnStaminaChanged;

    //public InputActionTrace inputActionTrace;

    [NonSerialized]
    public Animator animator;

    [NonSerialized]
    public Rigidbody2D rigid;

    public bool interuptible,
    canDash,
    canAttack,
    isPlayer1,
    isDashing;

    [NonSerialized]
    public LayerMask ground;

    public float groundSpeed,
     dashSpeed;

    public int dashFrameLength,
    dashStartup,
    nNeutralGFrames,
    nSideGFrames,
    nUpGFrames,
    nDownGFrames;

    public static int HIT_STUN_FRAME_LENGTH = 15;

    public float moveInput = 0;

    private int health = 10;

    private int stamina = 10;

    //Low, Mid, High: 0,1,2
    private int attackHeight = -1;

    private int currAttackValue = 0;

    //Low, Mid, High: 0,1,2
    private int guardHeight = -1;

    public UnityEngine.Rendering.Universal.Light2D glowLight;

    public GameObject colliders;

    public BoxCollider2D playerCollider;

    public BoxCollider2D charBlockerCollider;

    private Renderer renderer;

    private Shader normalShader, outlineShader;

    public Queue buffer;
    private double elapsedTime;

    public IdleState idleState;
    public DashState dashState;
    public RunState runState;
    public HitStunState hitStunState;
    public BlockStunState blockStunState;
    public NormalAttackState normalAttackState;
    public SpecialAttackState specialAttackState;
    public GuardState guardState;
    public ParryState parryState;

    public int Health
    { 
        get => health;
        set {
            health = value;
            if(OnHealthChanged != null)
            {
                OnHealthChanged();
            }
        }
    }

    public int Stamina
    {
        get => stamina;
        set
        {
            stamina = value;
            if (OnStaminaChanged != null)
            {
                OnStaminaChanged();
            }
        }
    }

    public int CurrAttackValue { get => currAttackValue; set => currAttackValue = value; }
    public int AttackHeight { get => attackHeight; set => attackHeight = value; }
    public int GuardHeight { get => guardHeight; set => guardHeight = value; }

    #region Animation Names

    //get by hash, more optimized
    [NonSerialized]
    public string aIdleAnim = "Base Layer.Advntr-Idle",
    aDashAnim = "Base Layer.Advntr-Dash",
    aRunAnim = "Base Layer.Advntr-Run",
    aHitStunAnim = "Base Layer.Advntr-HitStun",
    aGroundGuardAnim = "Base Layer.Advntr-GroundGuard",
    aAirGuardAnim = "Base Layer.Advntr-AirGuard",
    aNNeutralGroundAnim = "Base Layer.Advntr-NormalNeutralGround",
    aNSideGroundAnim = "Base Layer.Advntr-NormalSideGround",
    aNUpGroundAnim = "Base Layer.Advntr-NormalUpGround",
    aNDownGroundAnim = "Base Layer.Advntr-NormalDownGround",
    aSNeutralGroundAnim = "Base Layer.Advntr-SpecialNeutralGround",
    aSSideGroundAnim = "Base Layer.Advntr-SpecialSideGround",
    aSUpGroundAnim = "Base Layer.Advntr-SpecialUpGround",
    aSDownGroundAnim = "Base Layer.Advntr-SpecialDownGround";

    #endregion

    private void Awake()
    {
        isPlayer1 = gameObject.name.Contains("P1");

        if(isPlayer1)
        {
            CharManager.player1 = this;
        }
        else
        {
            CharManager.player2 = this;
        }

        buffer = new Queue(60);

        idleState = new IdleState();
        hitStunState = new HitStunState();
        blockStunState = new BlockStunState();
        runState = new RunState();
        dashState = new DashState();
        normalAttackState = new NormalAttackState();
        specialAttackState = new SpecialAttackState();
        guardState = new GuardState();
        parryState = new ParryState();

        playerInput = GetComponent<PlayerInput>();
        //inputActionTrace = new InputActionTrace();

        //charControls = new CharacterControls();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();

        stateMachine = new StateMachine<CharController>();
        stateMachine.Configure(this, idleState);

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

        stateSerializationHelper = stateMachine.GetCurrentState().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(buffer.Count);

        moveInput =
            playerInput.
            actions.
            FindAction("Move").ReadValue<float>();

        if (buffer.Count > 0)
        {
            InputAction currentInputAction = (InputAction)buffer.Peek();

            if(currentInputAction.name == "Move")
            {
                EnterState(currentInputAction.name);
            }
            else
            {
                EnterState(currentInputAction.name);
            }

            elapsedTime += Time.deltaTime;

            if(elapsedTime > 0.05 && buffer.Count > 0)
            {
                elapsedTime = 0;
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

            case State.Dash:
                if (!isGrounded && canDash)
                {
                    airDashInput = charControls.Character.Dash.ReadValue<float>();
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

    public void EnterState(string inputActionName)
    {
        IState<CharController> currState = stateMachine.GetCurrentState();

        if(!interuptible)
        {
            return;
        }

        switch(inputActionName)
        {
            case "Move":
                if (currState != runState)
                {
                    EnterState(runState);
                    buffer.Dequeue();
                }
                break;
            case "Dash":
                if (currState != dashState &&
                    canDash && stamina >= 1/*&& currState != blockStunState*/)
                {
                    EnterState(dashState);
                    buffer.Dequeue();
                }
                break;
            case "Heavy Normal":
                if (currState != normalAttackState)
                {
                    EnterState(normalAttackState);
                    buffer.Dequeue();
                }
                break;
            case "Guard":
                if (currState != guardState)
                {
                    EnterState(guardState);
                    buffer.Dequeue();
                }
                break;
            case "HitStun":
                if (currState != hitStunState)
                {
                    EnterState(hitStunState);
                }
                break;
            case "Regular Parry":
                EnterState(parryState);
                break;
            case "Normal Parry":
                EnterState(parryState);
                break;
            case "Heavy Parry":
                EnterState(parryState);
                break;
        }
    }

    public void EnterState(IState<CharController> stateEntered)
    {
        stateMachine.EnterState(stateEntered);
        stateSerializationHelper = stateMachine.GetCurrentState().ToString();
    }

    /*
    public void Movement(InputAction.CallbackContext context)
    {
        if(isGrounded && interuptible && moveInput != 0)
        {
            EnterState(runState);
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(stateMachine.GetCurrentState() != dashState && interuptible)
        {
            EnterState(dashState);
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
    */

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext obj)
    {
        if(buffer.Count >= 60)
        {
            buffer.Dequeue();
        }

        if(obj.action.name != "DirectionalInput")
        {
            buffer.Enqueue(obj.action);
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

                case State.Dash:
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

    public void ToggleMatrixCollision()
    {
        if (Physics2D.GetIgnoreLayerCollision(7, 8))
        {
            Physics2D.IgnoreLayerCollision(7, 8, false);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(7, 8, true);
        }
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
