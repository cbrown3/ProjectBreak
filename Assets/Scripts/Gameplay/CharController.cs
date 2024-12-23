using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Windows;

namespace FightLogic
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharController : MonoBehaviour
    {
        public static int HIT_STUN_FRAME_LENGTH = 15;

        public static double FRAME_LENGTH_SECONDS = 0.1666666666666666666666666667;

        public static int INPUT_BUFFER_FRAME_LENGTH = 3;

        [SerializeField]
        private Vector2 velocitySerializationHelper = Vector2.zero;

        private CharacterControls charControls;

        //Low, Mid, High: 0,1,2
        [SerializeField]
        private Height attackHeight = 0;

        private int currAttackValue = 0;

        //Low, Mid, High: 0,1,2
        [SerializeField]
        private Height guardHeight = 0;

        private int currGrabValue = 1;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        private Renderer renderer;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        private Shader normalShader, outlineShader;

        private double elapsedBufferTime;

        public UnityEngine.Rendering.Universal.Light2D glowLight;

        public StateMachine<CharController> stateMachine;

        [SerializeField]
        public PlayerInput playerInput;

        [SerializeField]
        public PlayerData playerData;

        public Animator animator;

        public Rigidbody2D rigid;

        public bool interuptible,
        canDash,
        canAttack,
        isPlayer1,
        isDashing,
        canAttackCancel;

        public float groundSpeed,
         dashSpeed;

        public int dashFrameLength,
        dashStartup,
        pushbackFrameLength,
        throwFrameLength;

        public AttackSpeed nNeutralGFrames,
        nSideGFrames,
        nUpGFrames,
        nDownGFrames;

        public float moveInput = 0;

        public GameObject colliders;

        public BoxCollider2D playerCollider;

        public Queue buffer;

        public IdleState idleState;
        public DashState dashState;
        public RunState runState;
        public HitStunState hitStunState;
        public BlockStunState blockStunState;
        public NormalAttackState normalAttackState;
        public SpecialAttackState specialAttackState;
        public GuardState guardState;
        public ParryState parryState;
        public ThrowState throwState;
        public ThrownState thrownState;
        public GrabState grabState;
        public PushbackState pushbackState;
        public HardKnockdownState hardKnockdownState;
        public SoftKnockdownState softKnockdownState;

        public StateType StateType = StateType.None;

        public int CurrAttackValue { get => currAttackValue; set => currAttackValue = value; }
        public int CurrGrabValue { get => currGrabValue; set => currGrabValue = value; }
        public Height AttackHeight { get => attackHeight; set => attackHeight = value; }
        public Height GuardHeight { get => guardHeight; set => guardHeight = value; }

        public enum Height
        {
            None,
            Low,
            Mid,
            High
        }

        public enum AttackSpeed
        {
            None = 0,
            Fast = 10,
            Medium = 16,
            Slow = 25
        }

        #region Animation Names

        //get by hash, more optimized
        [NonSerialized]
        public string aIdleAnim = "Base Layer.Advntr-Idle",
        aDashAnim = "Base Layer.Advntr-Dash",
        aRunAnim = "Base Layer.Advntr-Run",
        aHitStunAnim = "Base Layer.Advntr-HitStun",
        aGroundGuardAnim = "Base Layer.Advntr-GroundGuard",
        aNNeutralGroundAnim = "Base Layer.Advntr-NormalNeutralGround",
        aNSideGroundAnim = "Base Layer.Advntr-NormalSideGround",
        aNUpGroundAnim = "Base Layer.Advntr-NormalUpGround",
        aNDownGroundAnim = "Base Layer.Advntr-NormalDownGround",
        aSNeutralGroundAnim = "Base Layer.Advntr-SpecialNeutralGround",
        aSSideGroundAnim = "Base Layer.Advntr-SpecialSideGround",
        aSUpGroundAnim = "Base Layer.Advntr-SpecialUpGround",
        aSDownGroundAnim = "Base Layer.Advntr-SpecialDownGround",
        aParryAnim = "Base Layer.Advntr-Parry",
        aGrabAnim = "Base Layer.Advntr-Grab",
        aThrowAnim = "Base Layer.Advntr-Throw",
        aThrownAnim = "Base Layer.Advntr-Thrown",
        aSoftKnockdownAnim = "Base Layer.Advntr-SoftKnockdown",
        aHardKnockdownAnim = "Base Layer.Advntr-HardKnockdown";

        #endregion

        private void Awake()
        {
            isPlayer1 = gameObject.name.Contains("P1");

            if (isPlayer1)
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
            throwState = new ThrowState();
            thrownState = new ThrownState();
            grabState = new GrabState();
            pushbackState = new PushbackState();
            softKnockdownState = new SoftKnockdownState();
            hardKnockdownState = new HardKnockdownState();

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

            playerData.Stamina = 10;
            playerData.Health = 10;
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

                EnterState(currentInputAction.name);

                elapsedBufferTime += Time.deltaTime;

                //3 Frame Input Buffer: at 60FPS (0.16667 seconds per frame)
                //Inputs are entered for 3 frames until it is removed from the queue of inputs

                //Check for buffer size again since buffer can be empty after entering a state with certain actions, i.e. movement
                if (elapsedBufferTime > 0.05 && buffer.Count > 0)
                {
                    elapsedBufferTime = 0;
                    buffer.Dequeue();
                }
            }
            else
            {
                elapsedBufferTime = 0;
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

            if (canAttackCancel)
            {
                Debug.Log("Begin Attack Cancel!");
                EnterState(normalAttackState);
                return;
            }

            if (!interuptible)
            {
                return;
            }

            switch (inputActionName)
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
                        canDash && playerData.Stamina >= 1)
                    {
                        EnterState(dashState);
                        //buffer.Dequeue();
                    }
                    break;
                case "Heavy Normal":
                    if (canAttack)
                    {
                        EnterState(normalAttackState);
                        //buffer.Dequeue();
                    }
                    break;
                case "Guard":
                    if (currState != guardState)
                    {
                        EnterState(guardState);
                        //buffer.Dequeue();
                    }
                    break;
                case "Grab":
                    EnterState(grabState);
                    //buffer.Dequeue();
                    break;
                case "Regular Parry":
                    EnterState(parryState);
                    //buffer.Dequeue();
                    break;
                case "Normal Parry":
                    EnterState(parryState);
                    //buffer.Dequeue();
                    break;
                case "Heavy Parry":
                    EnterState(parryState);
                    //buffer.Dequeue();
                    break;
                case "Grab Parry":
                    EnterState(parryState);
                    //buffer.Dequeue();
                    break;
            }
        }

        public void EnterState(IState<CharController> stateEntered)
        {
            stateMachine.EnterState(stateEntered);
        }

        public void EnterState(StateType stateType)
        {
            switch (stateType)
            {
                case StateType.NormalAttack:
                    stateMachine.EnterState(normalAttackState);
                    break;
                case StateType.SpecialAttack:
                    stateMachine.EnterState(specialAttackState);
                    break;
                case StateType.Run:
                    stateMachine.EnterState(runState);
                    break;
                case StateType.Guard:
                    stateMachine.EnterState(guardState);
                    break;
                case StateType.Grab:
                    stateMachine.EnterState(grabState);
                    break;
                case StateType.Throw:
                    stateMachine.EnterState(throwState);
                    break;
                case StateType.Thrown:
                    stateMachine.EnterState(thrownState);
                    break;
                case StateType.Pushback:
                    stateMachine.EnterState(pushbackState);
                    break;
                case StateType.Dash:
                    stateMachine.EnterState(dashState);
                    break;
                case StateType.HitStun:
                    stateMachine.EnterState(hitStunState);
                    break;
                case StateType.BlockStun:
                    stateMachine.EnterState(blockStunState);
                    break;
                case StateType.RegularParry:
                case StateType.NormalParry:
                case StateType.SpecialParry:
                case StateType.GrabParry:
                    stateMachine.EnterState(parryState);
                    break;
                case StateType.SoftKnockdown:
                    stateMachine.EnterState(softKnockdownState);
                    break;
                case StateType.HardKnockdown: 
                    stateMachine.EnterState(hardKnockdownState);
                    break;
                case StateType.None:
                case StateType.Idle:
                default:
                    stateMachine.EnterState(idleState);
                    break;
            }
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
            //Size of Input Buffer is 5:
            //Each input takes 3 frames to deque, 5 * 3 = 60fps/4 = 0.25 seconds of input queue delay
            if (buffer.Count >= 5)
            {
                buffer.Dequeue();
            }

            if (obj.action.name != "DirectionalInput")
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
            if (collision.tag == "MainCamera")
            {
                Debug.Log("leaving cam");
                collision.transform.Translate(0, 0, 0);
            }
        }
    }
}