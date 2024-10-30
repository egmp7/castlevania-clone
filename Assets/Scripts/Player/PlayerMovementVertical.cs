using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementVertical : MonoBehaviour
{
    public static event Action OnJump;
    public static event Action OnGround;
    public static event Action OnAir;
    public static event Action OnFall;
    public static event Action OnAscend;

    [Header("Basic Jump")]

    [Tooltip("Jumping Strength")]
    [SerializeField] int JumpForce = 25;

    [SerializeField] Transform GroundDetector;
    [SerializeField] Vector2 BoxcastSize = new(2f,2f);

    [Tooltip("Cooldown time between jumps (in seconds)")]
    [SerializeField][Range (0f,3f)] float jumpCooldown = 0.8f;

    [SerializeField] LayerMask GroundLayer;

    private PlayerState playerState;
    private InputAction moveAction;
    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isComponentActive;
    private float jumpTimer;

    // event guards
    private bool isOnJumpEventTriggered;
    private bool isOnGroundEventTriggered;
    private bool isOnAirEventTriggered;
    private bool isOnFallEventTriggered;
    private bool isOnAscendEventTriggered;

    private void OnEnable()
    {
        PlayerLedgeClimb.OnLedgeHang += DeactivateComponent;
        PlayerLedgeClimb.OnLedgeRelease += ActivateComponent;
    }
    private void OnDisable()
    {
        PlayerLedgeClimb.OnLedgeHang -= DeactivateComponent;
        PlayerLedgeClimb.OnLedgeRelease -= ActivateComponent;
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        playerState = GetComponent<PlayerState>();
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isComponentActive = true;
    }

    private void Update()
    {
        if (!isComponentActive) return;

        CheckGroundStatus();
        CheckVelocityDirection();
        HandleInput();
        UpdateJumpTimer();
    }

    private void FixedUpdate()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        // Only jump if the key is held down, the player is grounded, and the jump cooldown has passed
        if (isOnJumpEventTriggered && playerState.IsOnGround && jumpTimer <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            isOnJumpEventTriggered = false; // Ensure single jump per press
            jumpTimer = jumpCooldown; // Reset the jump timer to cooldown value
            playerState.IsJumping = true;
            Debug.Log("OnJump");
            OnJump?.Invoke();
        }
    }

    private void HandleInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.y > 0 && !isOnJumpEventTriggered)
        {
            isOnJumpEventTriggered = true; // Key pressed
        }
        else if (moveInput.y == 0)
        {
            isOnJumpEventTriggered = false; // Key released
        }
            
    }

    private void CheckGroundStatus()
    {
        Bounds bounds = playerCollider.bounds;

        RaycastHit2D hit = Physics2D.BoxCast(
            GroundDetector.position,
            BoxcastSize,
            0f,
            Vector2.right,
            0f,
            GroundLayer);

        playerState.IsOnGround = hit.collider != null;

        if (playerState.IsOnGround && !isOnGroundEventTriggered) // is grounded
        {
            isOnGroundEventTriggered = true;
            isOnAirEventTriggered = false;
            Debug.Log("OnGround");
            OnGround?.Invoke();
        }
        else if (!playerState.IsOnGround && !isOnAirEventTriggered) // is on air
        {
            isOnGroundEventTriggered = false;
            isOnAirEventTriggered = true;
            //Debug.Log("OnAir");
            OnAir?.Invoke();
        }
    }

    void CheckVelocityDirection()
    {
        if (rb.velocity.y > 0 && !isOnAscendEventTriggered && !playerState.IsOnGround)
        {
            isOnAscendEventTriggered = true;
            isOnFallEventTriggered = false;
            //Debug.Log("OnAscend");
            OnAscend?.Invoke();  // Player is moving upward
        }
        else if (rb.velocity.y < 0 && !isOnFallEventTriggered && !playerState.IsOnGround)
        {
            isOnAscendEventTriggered = false;
            isOnFallEventTriggered = true;
            Debug.Log("OnFall");
            OnFall?.Invoke();  // Player is falling
        }
        else if(rb.velocity.y == 0)
        {
            isOnAscendEventTriggered = false;
            isOnFallEventTriggered = false;
        }
    }

    private void UpdateJumpTimer()
    {
        // Decrease the jump timer each frame, if greater than 0
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void ActivateComponent()
    {
        isComponentActive = true;
    }

    private void DeactivateComponent()
    {
        isComponentActive = false;
    }

    private void OnDrawGizmos()
    {
        if (playerState == null) return;
        Gizmos.color = playerState.IsOnGround ? Color.red : Color.green;
        Gizmos.DrawWireCube(GroundDetector.position, BoxcastSize);
    }

}
