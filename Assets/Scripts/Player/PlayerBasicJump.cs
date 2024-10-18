using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicJump : MonoBehaviour
{
    public static event Action OnPlayerJump;
    public static event Action OnPlayerGrounded;

    [Header("Basic Jump")]

    [Tooltip("Jumping Strength")]
    [SerializeField] int JumpForce = 25;

    [Tooltip("Distance that expands the jumping boxcast")]
    [SerializeField][Range(0f, 2f)] float boxCastDistance = 1f;

    [Tooltip("Time between jumps (in seconds)")]
    [SerializeField][Range (0f,3f)] float jumpCooldown = 0.8f; // Cooldown time in seconds

    [SerializeField] LayerMask GroundLayer;
    [SerializeField] bool DebugMode = true;

    private InputAction moveAction;
    private Collider2D playerCollider;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool keyHeldDown;
    private bool isGrounded;
    private bool isInvoked = false;
    private float jumpTimer;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        GroundCheck();
        UpdateJumpTimer();
        InvokePlayerGrounded();
    }

    private void FixedUpdate()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        // Only jump if the key is held down, the player is grounded, and the jump cooldown has passed
        if (keyHeldDown && isGrounded && jumpTimer <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            keyHeldDown = false; // Ensure single jump per press
            jumpTimer = jumpCooldown; // Reset the jump timer to cooldown value
            OnPlayerJump?.Invoke();
        }
    }

    private void HandleInput()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.y > 0 && !keyHeldDown)
        {
            keyHeldDown = true; // Key pressed
        }
        else if (moveInput.y == 0)
        {
            keyHeldDown = false; // Key released
        }
            
    }

    private void InvokePlayerGrounded()
    {
        if (isGrounded && !isInvoked)
        {
            OnPlayerGrounded?.Invoke();
            isInvoked = true;
        }
        else if (!isGrounded && isInvoked)
        {
            isInvoked = false;
        }
    }

    private void GroundCheck()
    {
        Bounds bounds = playerCollider.bounds;

        // Perform BoxCast to check ground collision
        RaycastHit2D hit = Physics2D.BoxCast(
            bounds.center,
            bounds.size,
            0f,
            Vector2.down,
            boxCastDistance,
            GroundLayer
        );

        // Optional debug visualization
        if (DebugMode)
        {
            Utilities.DebugDrawBoxCast(
                bounds.center,
                bounds.size,
                0f,
                Vector2.down,
                boxCastDistance,
                hit.collider ? Color.green : Color.red
            );
        }

        // Set grounded state based on collision result
        isGrounded = hit.collider != null;
    }

    private void UpdateJumpTimer()
    {
        // Decrease the jump timer each frame, if greater than 0
        if (jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

}
