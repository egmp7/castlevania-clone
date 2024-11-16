using InputCommands.Move;
using System;
using UnityEngine;

namespace Player.StateManagement
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(DirectionMapper))]

    public class StateController : MonoBehaviour
    {
        [HideInInspector] public Rigidbody2D rigidBody;
        [HideInInspector] public Animator animator;

        // Ground Class 
        [HideInInspector] public DirectionMapper directionMapper;
        [HideInInspector] public Vector3 originalScale;

        // states
        private readonly IdleState idleState = new();
        private readonly FallState fallState = new();
        private readonly JumpState jumpState = new();
        private readonly WalkState walkState = new();
        private readonly RunState runState = new();
        private readonly CrouchState crouchState = new();
        private readonly PunchState punchState = new();
        private readonly KickState kickState = new();

        private State currentState;
        private bool isOnGround;

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
        [Tooltip("Reset time of the punch combo")]
        [Range(0.0f, 2f)] public float PunchComboResetTime = 1f;
        [Tooltip("Reset time of the punch combo")]
        [Range(0.0f, 2f)] public float KickComboResetTime = 0.8f;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            directionMapper = GetComponent<DirectionMapper>();
        }

        private void Start()
        {
            ChangeState(idleState);
            originalScale = transform.localScale;
        }

        void Update()
        {
            CheckGroundStatus();
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

        public void Idle()
        {
            if (currentState is AttackState) return;
            if (isOnGround) ChangeState(idleState);
        }

        public void Walk()
        {
            if (currentState is AttackState) return;
            if (isOnGround) ChangeState (walkState);
        }

        public void Run()
        {
            if (currentState is AttackState) return;
            if (isOnGround) ChangeState(runState);
        }

        public void Jump()
        {
            if (currentState is AttackState) return;
            if (isOnGround) ChangeState(jumpState);
        }

        public void Crouch()
        {
            if (currentState is AttackState) return;
            if (isOnGround) ChangeState(crouchState);
        }


        public void Punch()
        {
            ChangeState(punchState);
            punchState.OnStateAttack();
        }

        public void Kick()
        {
            ChangeState(kickState);
            kickState.OnStateAttack();
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