using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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

    public bool interuptible,
    isGrounded,
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
    nNeutralGFrames,
    nSideGFrames,
    nUpGFrames,
    nDownGFrames,
    nNeutralAFrames,
    nSideAFrames,
    nUpAFrames,
    nDownAFrames;

    public float jumpInput, moveInput;

    public Light2D glowLight;

    public GameObject colliders;

    private Renderer renderer;

    private Shader normalShader, outlineShader;

    /*
    public StopState stopState;
    public AirDashState airDashState;
    public RunState runState;
    public JumpState jumpState;
    public FallState fallState;
    */

    #region Animation Names

    [NonSerialized]
    public string aIdleAnim = "Base Layer.Advntr-Idle",
    aAirDashAnim = "Base Layer.Advntr-AirDash",
    aRunAnim = "Base Layer.Advntr-Run",
    aJumpAnim = "Base Layer.Advntr-Jump",
    aFallAnim = "Base Layer.Advntr-Fall",
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
        charControls = new CharacterControls();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();

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
        normalShader = Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default");
        outlineShader = Shader.Find("Shader Graphs/PlayerOutline");

        interuptible = true;

        charControls.Character.Jump.performed += _ => EnterState(JumpState.Instance);
        charControls.Character.AirDash.performed += _ => AirDash();
        charControls.Character.Move.performed += _ => Movement();
        charControls.Character.LightNormal.performed += _ => Attack(NormalAttackState.Instance);
        charControls.Character.HeavyNormal.performed += _ => Attack(NormalAttackState.Instance);
        //charControls.Character.LightSpecial.performed += _ => Attack(SpecialAttackState.Instance);
        //charControls.Character.HeavySpecial.performed += _ => Attack(SpecialAttackState.Instance);
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

    #region Enter State 

    public void EnterState(IState<CharController> stateEntered)
    {
        stateMachine.EnterState(stateEntered);
    }

    public void Movement()
    {
        if(isGrounded && interuptible)
        {
            EnterState(RunState.Instance);
        }
    }

    public void AirDash()
    {
        if(stateMachine.GetCurrentState() != AirDashState.Instance && interuptible)
        {
            EnterState(AirDashState.Instance);
        }
    }

    public void Attack(IState<CharController> state)
    {
        if(canAttack && interuptible)
        {
            EnterState(state);
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

        renderer.sharedMaterial.SetColor("_Color", Color.cyan);
        renderer.sharedMaterial.shader = outlineShader;
    }

    public void NormalAttackGlow()
    {
        glowLight.color = Color.red;
        glowLight.gameObject.SetActive(true);

        renderer.sharedMaterial.SetColor("_Color", Color.red);
        renderer.sharedMaterial.shader = outlineShader;
    }

    public void ResetGlow()
    {
        glowLight.gameObject.SetActive(false);

        renderer.sharedMaterial.shader = normalShader;
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
