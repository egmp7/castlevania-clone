using UnityEngine;

public class StateController : MonoBehaviour
{
    [HideInInspector] public State currentState;
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public InputController inputController;

    // states
    [HideInInspector] public IdleState idleState = new();
    [HideInInspector] public FallState fallState = new();
    [HideInInspector] public JumpState jumpState = new();
    [HideInInspector] public WalkState walkState = new();
    [HideInInspector] public RunState runState = new();

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


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        inputController = GetComponent<InputController>();
    }

    private void Start()
    {
        ChangeState(idleState);
    }

    void Update()
    {
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

    private void OnDrawGizmos()
    {
        if (idleState == null) return;
        Gizmos.color = idleState.isOnGround ? Color.red : Color.green;
        Gizmos.DrawWireCube(groundDetector.position, groundDetectorSize);
    }
}