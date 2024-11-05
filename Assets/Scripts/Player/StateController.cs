using System;
using UnityEngine;

namespace Player.StateManagement
{
    [RequireComponent(typeof(InputSystemController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]

    public class StateController : MonoBehaviour
    {
        [HideInInspector] public State currentState;
        [HideInInspector] public Rigidbody2D rigidBody;
        [HideInInspector] public InputSystemController inputSystemController;
        [HideInInspector] public Animator animator;
        [HideInInspector] public bool isOnGround;
        [HideInInspector] public int facing;
        [HideInInspector] public Vector3 originalScale;

        // states
        [HideInInspector] public IdleState idleState = new();
        [HideInInspector] public FallState fallState = new();
        [HideInInspector] public JumpState jumpState = new();
        [HideInInspector] public WalkState walkState = new();
        [HideInInspector] public RunState runState = new();
        [HideInInspector] public CrouchState crouchState = new();
        [HideInInspector] public PlayerPunch01State playerPunch01State = new();
        [HideInInspector] public PlayerPunch02State playerPunch02State = new();
        [HideInInspector] public PlayerPunch03State playerPunch03State = new();
        [HideInInspector] public PlayerKick01State playerKick01State = new();
        [HideInInspector] public PlayerKick02State playerKick02State = new();
        [HideInInspector] public PlayerKick03State playerKick03State = new();


        [Header("X Movement")]
        [Range(1.0f, 50.0f)] public float walkSpeed = 10.0f;
        [Range(1.0f, 50.0f)] public float runSpeed = 20.0f;
        [Range(0.1f, 10.0f)] public float accelerationRate = 5f;

        [Header("Basic Jump")]
        [Tooltip("Jumping Strength")]
        public int jumpForce = 25;
        public Transform groundDetector;
        [Tooltip("Size of the GroundDetector")]
        public Vector2 groundDetectorSize = new(2f, 2f);
        [Tooltip("Cooldown time between jumps")]
        [Range(0f, 3f)]
        public float jumpCooldown = 0.8f;
        public LayerMask groundLayer;

        [Header("Attack")]
        [SerializeField] LayerMask EnemyLayer;
        [SerializeField] Transform EnemyDetectorPosition;
        [Tooltip("Radius of the Overlap Circle Attack")]
        [SerializeField][Range(0.01f, 1f)] float EnemyDetectorRadius = 0.25f;

        [Header("Combo Settings")]
        public float comboResetTime = 1.0f;  // Time to reset the combo if no new attack is made
        private int maxPunchCombo = 3;             // Number of attack stages in the combo
        private int punchCurrentCombo = 0;
        private int maxKickCombo = 3;             // Number of attack stages in the combo
        private int kickCurrentCombo = 0;
        private float lastPunchAttackTime;
        private float lastKickAttackTime;

        private void OnEnable()
        {
            InputSystemController.OnAttack += OnAttack1InputSystem;
            InputSystemController.OnKick += OnAttack2InputSystem;
        }
        private void OnDisable()
        {
            InputSystemController.OnAttack -= OnAttack1InputSystem;
            InputSystemController.OnKick -= OnAttack2InputSystem;
        }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            inputSystemController = GetComponent<InputSystemController>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            ChangeState(idleState);
            originalScale = transform.localScale;
        }

        void Update()
        {
            CheckGroundStatus();
            CheckFacing();
            SelectState();
            ResetComboValues();

            currentState?.OnStateUpdate();
        }

        private void FixedUpdate()
        {
            currentState?.OnStateFixedUpdate();
        }

        public void ChangeState(State newState)
        {
            if (newState == null)
            {
                Debug.LogError("New state cannot be null");
                return;
            }

            if (currentState == newState) return;

            currentState?.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter(this);
            Debug.Log(currentState);
        }

        private void SelectState()
        {
            InputSystemController.MoveState currentMoveState = inputSystemController.currentMoveState;

            if (currentState is AttackState) return;

            if (isOnGround)
            {
                if (
                    currentMoveState == InputSystemController.MoveState.Left ||
                    currentMoveState == InputSystemController.MoveState.Right)
                {
                    if (inputSystemController.isDoubleTap)
                    {
                        ChangeState(runState);
                    }
                    else
                    {
                        ChangeState(walkState);
                    }
                }

                else if (
                    currentMoveState == InputSystemController.MoveState.Up ||
                    currentMoveState == InputSystemController.MoveState.UpLeft ||
                    currentMoveState == InputSystemController.MoveState.UpRight)
                {
                    ChangeState(jumpState);
                }
                else if (
                    currentMoveState == InputSystemController.MoveState.Down ||
                     currentMoveState == InputSystemController.MoveState.DownLeft ||
                      currentMoveState == InputSystemController.MoveState.DownRight
                    )
                {
                    ChangeState(crouchState);
                }
                else
                {
                    ChangeState(idleState);
                }
            }
            else // on air
            {
                if (rigidBody.velocity.y < 0)
                {
                    ChangeState(fallState);
                }
            }
        }

        private void CheckGroundStatus()
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                groundDetector.position,
                groundDetectorSize,
                0f,
                Vector2.right,
                0f,
                groundLayer);

            isOnGround = hit.collider != null;
        }

        private void CheckFacing()
        {
            if (inputSystemController.currentMoveState == InputSystemController.MoveState.Right ||
                inputSystemController.currentMoveState == InputSystemController.MoveState.UpRight ||
                inputSystemController.currentMoveState == InputSystemController.MoveState.DownRight)
            {
                facing = 1;
            }
            if (inputSystemController.currentMoveState == InputSystemController.MoveState.Left ||
                inputSystemController.currentMoveState == InputSystemController.MoveState.UpLeft ||
                inputSystemController.currentMoveState == InputSystemController.MoveState.DownLeft)
            {
                facing = -1;
            }
        }

        public void OnAttack1InputSystem()
        {
            if (!isOnGround) return;

            kickCurrentCombo = 0;

            ExecuteCombo(ref punchCurrentCombo, ref lastPunchAttackTime, maxPunchCombo, new Action[]
            {
        () => ChangeState(playerPunch01State),
        () => ChangeState(playerPunch02State),
        () => ChangeState(playerPunch03State),
            });
        }

        public void OnAttack2InputSystem()
        {
            if (!isOnGround) return;

            punchCurrentCombo = 0;

            ExecuteCombo(ref kickCurrentCombo, ref lastKickAttackTime, maxKickCombo, new Action[]
            {
        () => ChangeState(playerKick01State),
        () => ChangeState(playerKick02State),
        () => ChangeState(playerKick03State),
            });
        }

        private void ExecuteCombo(ref int currentCombo, ref float lastAttackTime, int maxPunchCombo, Action[] comboStates)
        {
            lastAttackTime = Time.time;

            // Cycle through combo stages
            currentCombo = (currentCombo % maxPunchCombo) + 1;

            Debug.Log("Current Combo: " + currentCombo);

            // Invoke the corresponding action for the current combo stage
            if (currentCombo - 1 < comboStates.Length)
            {
                comboStates[currentCombo - 1].Invoke();
            }
        }

        private void ResetComboValues()
        {
            // Check if combo should be reset
            if (Time.time - lastPunchAttackTime > comboResetTime)
            {
                punchCurrentCombo = 0;
            }

            if (Time.time - lastKickAttackTime > comboResetTime)
            {
                kickCurrentCombo = 0;
            }
        }

        public void OnAnimationEnd()
        {
            Debug.Log("OnAttackAnimationEnd");
            ChangeState(idleState);
        }

        public void OnAttackAnimation()
        {
            Collider2D hit = Physics2D.OverlapCircle(
                EnemyDetectorPosition.position,
                EnemyDetectorRadius,
                EnemyLayer);

            if (hit != null)
            {

                if (hit.TryGetComponent<EnemyHP>(out var enemyHP))
                {
                    enemyHP.TakeDamage(30f);
                    Debug.Log("Enemy Touched");
                }
                else
                {
                    Debug.LogError("No EnemyHP component found on hit object!");
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = isOnGround ? Color.red : Color.green;
            Gizmos.DrawWireCube(groundDetector.position, groundDetectorSize);
            Gizmos.DrawWireSphere(EnemyDetectorPosition.position, EnemyDetectorRadius);
        }
    }
}