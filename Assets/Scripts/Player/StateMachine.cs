using InputCommands.Move;
using System;
using UnityEngine;

namespace Player.StateManagement
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(DirectionMapper))]

    public class StateMachine : MonoBehaviour
    {
        private State currentState;

        // States
        private readonly IdleState idleState = new();
        private readonly FallState fallState = new();
        private readonly JumpState jumpState = new();
        private readonly WalkState walkState = new();
        private readonly RunState runState = new();
        private readonly CrouchState crouchState = new();
        private readonly PunchState punchState = new();
        private readonly KickState kickState = new();

        // Iputs
        [HideInInspector] public Rigidbody2D rigidBody;
        [HideInInspector] public Animator animator;
        [HideInInspector] public DirectionMapper directionMapper;
        [HideInInspector] public Vector3 originalScale;

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

        private void Update()
        {
            currentState?.OnStateUpdate();
        }

        private void FixedUpdate()
        {
            currentState?.OnStateFixedUpdate();
        }

        public void ChangeState(State newState)
        {
            #region Change State Algo
            if (newState == null)
            {
                Debug.LogError("New state cannot be null");
                return;
            }

            if (currentState == newState) return;

            currentState?.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter(this);
            #endregion
            Debug.Log(currentState);
        }

        public void Idle()
        {
            if (currentState is AttackState) return;
            ChangeState(idleState);
        }

        public void Walk()
        {
            if (currentState is AttackState) return;
            ChangeState (walkState);
        }

        public void Run()
        {
            if (currentState is AttackState) return;
            ChangeState(runState);
        }

        public void Jump()
        {
            if (currentState is AttackState) return;
            ChangeState(jumpState);
        }

        public void Crouch()
        {
            if (currentState is AttackState) return;
            ChangeState(crouchState);
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

        public void Fall()
        {
            ChangeState(fallState);
        }

        public void OnAnimationEnd()
        {
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
            Gizmos.DrawWireSphere(EnemyDetectorPosition.position, EnemyDetectorRadius);
        }
    }
}