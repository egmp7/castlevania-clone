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

    [Tooltip("Time between jumps (in seconds)")]
    [SerializeField][Range (0f,3f)] float jumpCooldown = 0.8f; // Cooldown time in seconds

    [SerializeField] LayerMask GroundLayer;

    private InputAction moveAction;
    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private float jumpTimer;

    // event guards
    private bool isOnJumpEventTriggered;
    private bool isOnGroundEventTriggered;
    private bool isOnAirEventTriggered;
    private bool isOnFallEventTriggered;
    private bool isOnAscendEventTriggered;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
        if (isOnJumpEventTriggered && isGrounded && jumpTimer <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            isOnJumpEventTriggered = false; // Ensure single jump per press
            jumpTimer = jumpCooldown; // Reset the jump timer to cooldown value
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

        isGrounded = hit.collider != null;

        if (isGrounded && !isOnGroundEventTriggered) // is grounded
        {
            isOnGroundEventTriggered = true;
            isOnAirEventTriggered = false;
            Debug.Log("OnGround");
            OnGround?.Invoke();
        }
        else if (!isGrounded && !isOnAirEventTriggered) // is on air
        {
            isOnGroundEventTriggered = false;
            isOnAirEventTriggered = true;
            //Debug.Log("OnAir");
            OnAir?.Invoke();
        }

        // Set grounded state based on collision result
    }

    void CheckVelocityDirection()
    {
        if (rb.velocity.y > 0 && !isOnAscendEventTriggered)
        {
            isOnAscendEventTriggered = true;
            isOnFallEventTriggered = false;
            //Debug.Log("OnAscend");
            OnAscend?.Invoke();  // Player is moving upward
        }
        else if (rb.velocity.y < 0 && !isOnFallEventTriggered)
        {
            isOnAscendEventTriggered = false;
            isOnFallEventTriggered = true;
            Debug.Log("OnFall");
            OnFall?.Invoke();  // Player is falling
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

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.red : Color.green;
        Gizmos.DrawWireCube(GroundDetector.position, BoxcastSize);
    }

}
