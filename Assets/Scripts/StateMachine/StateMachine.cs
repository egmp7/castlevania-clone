using UnityEngine;

namespace Player.StateManagement
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]

    public class StateMachine : MonoBehaviour
    {
        private State _currentState;

        // States
        private readonly IdleState _stateIdle = new();
        private readonly FallState _stateFall = new();
        private readonly JumpState _stateJump = new();
        private readonly WalkState _stateWalk = new();
        private readonly RunState _stateRun = new();
        private readonly CrouchState _stateCrouch = new();
        private readonly PunchState _statePunch = new();
        private readonly KickState _stateKick = new();

        // Inputs
        [HideInInspector] public Rigidbody2D RigidBody;
        [HideInInspector] public Animator Animator;
        [HideInInspector] public Vector3 originalScale;
        [HideInInspector] public int direction;
        [HideInInspector] public bool debugDraw;

        [Header("X Movement")]
        [Range(1.0f, 50.0f)] public float walkSpeed = 10.0f;
        [Range(1.0f, 50.0f)] public float runSpeed = 20.0f;
        [Range(0.1f, 10.0f)] public float accelerationRate = 5f;

        [Header("Jump")]
        [Tooltip("Jumping Strength")]
        public int jumpForce = 25;

        [Header("Attack")]
        public LayerMask EnemyLayer;
        public Transform EnemyDetectorPosition;
        [Tooltip("Reset time of the punch combo")]
        [Range(0.0f, 2f)] public float punchComboResetTime = 1f;
        [Tooltip("Reset time of the punch combo")]
        [Range(0.0f, 2f)] public float kickComboResetTime = 0.8f;
        [Tooltip("Reset time of the punch combo")]
        public Vector2 attackOffset;


        private void Awake()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        private void Start()
        {
            // set up TODO GEt rid of this
            direction = 1;
            originalScale = transform.localScale;

            ChangeState(_stateIdle);
        }

        private void Update()
        {
            _currentState?.OnStateUpdate();
        }

        private void FixedUpdate()
        {
            _currentState?.OnStateFixedUpdate();
        }

        public void ChangeState(State newState)
        {
            #region Change State Algo
            if (newState == null)
            {
                Debug.LogError("New state cannot be null");
                return;
            }

            if (_currentState == newState) return;

            _currentState?.OnStateExit();
            _currentState = newState;
            _currentState.OnStateEnter(this);
            #endregion
            Debug.Log(_currentState);
        }

        public void Idle()
        {
            if (_currentState is AttackState) return;
            ChangeState(_stateIdle);
        }

        public void Walk()
        {
            if (_currentState is AttackState) return;
            ChangeState (_stateWalk);
        }

        public void Run()
        {
            if (_currentState is AttackState) return;
            ChangeState(_stateRun);
        }

        public void Jump()
        {
            if (_currentState is AttackState) return;
            ChangeState(_stateJump);
        }

        public void Crouch()
        {
            if (_currentState is AttackState) return;
            ChangeState(_stateCrouch);
        }

        public void Punch()
        {
            ChangeState(_statePunch);
            _statePunch.OnStateAttack();
        }

        public void Kick()
        {
            ChangeState(_stateKick);
            _stateKick.OnStateAttack();
        }

        public void Fall()
        {
            ChangeState(_stateFall);
        }

        public void FlipDirection()
        {
            direction = -direction;
        }

        public void OnAnimationEnd()
        {
            ChangeState(_stateIdle);
        }

        public void OnAttackAnimation()
        {
            if (_currentState is AttackState attackState)
            {
                attackState.AnimationAttack();
            }
        }

        private void OnDrawGizmos()
        {
            if (!debugDraw)
            {
                debugDraw = true;
            }
        }
    }
}