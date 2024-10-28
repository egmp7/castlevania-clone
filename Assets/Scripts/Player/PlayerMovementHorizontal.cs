using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHorizontal : MonoBehaviour
{
    public static event Action OnRun;
    public static event Action OnWalk;
    public static event Action OnIdle;
    public static event Action OnWallTouch;

    [Header("Movement")]
    [SerializeField][Range(1.0f, 50.0f)] float WalkSpeed = 10.0f;
    [SerializeField][Range(1.0f, 50.0f)] float RunSpeed = 20.0f;
    [SerializeField][Range(0.1f, 5.0f)] float AccelerationRate = 1f;
    [SerializeField][Range(0.05f, 1.0f)] float doubleTapThreshold = 0.3f;

    [Header("Wall Detector")]
    [Tooltip("Layer to detect walls")]
    [SerializeField] LayerMask WallLayer;
    [Tooltip("Transform of the wall detector game object")]
    [SerializeField] Transform WallDetector;
    [Tooltip("Size of the Boxcast area")]
    [SerializeField] Vector2 BoxcastSize = new(2f, 2f);

    private PlayerState playerState;
    private InputAction moveAction;
    private Rigidbody2D rb;
    private Vector3 originalScale; // Store the original scale
    private Vector2 moveInput;
    private float lastTapTime;
    private bool isComponentActive;
    
    // event guards
    private bool keyHeldDown;
    private bool isIdleEventTriggered;
    private bool isWallTouchEventTriggered;

    private void OnEnable()
    {
        PlayerLedgeClimb.OnLedgeHang += DeactivateComponent;
        PlayerLedgeClimb.OnLedgeRelease += ActivateComponent;
        
        PlayerAnimation.OnClimbAnimationEnded += ActivateComponent;
    }
    private void OnDisable()
    {
        PlayerLedgeClimb.OnLedgeHang -= DeactivateComponent;
        PlayerLedgeClimb.OnLedgeRelease -= ActivateComponent;

        PlayerAnimation.OnClimbAnimationEnded -= ActivateComponent;
    }

    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
        originalScale = transform.localScale; // Get the initial scale
    }

    private void Start()
    {
        isComponentActive = true;
    }

    private void Update()
    {
        if (!isComponentActive) return;

        FlipPlayerBasedOnDirection();
        HandleInput();
        DetectWall();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        // Debug.Log("Velocity: " + rb.velocity);
    }

    private void HandleMovement()
    {
        float targetSpeed = playerState.IsRunning ? RunSpeed : WalkSpeed;

        if (keyHeldDown && !playerState.IsTouchingWall)
        {
            // Lerp for smooth acceleration
            float currentSpeed = Mathf.Lerp(rb.velocity.x, moveInput.x * targetSpeed, AccelerationRate * Time.fixedDeltaTime);
            // Preserve the vertical velocity and update horizontal speed
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void HandleInput()
    {
        // Read horizontal input
        moveInput = moveAction.ReadValue<Vector2>();
        float moveInputHorizontal = moveInput.x;

        if (moveInputHorizontal != 0 && !keyHeldDown && !playerState.IsTouchingWall)
        {
            keyHeldDown = true;

            // Check for double tap
            if (Time.time - lastTapTime < doubleTapThreshold)
            {
                // Double tap detected, start running
                playerState.IsRunning = true;
                Debug.Log("OnRun");
                OnRun?.Invoke();
            }
            else
            {
                // Single tap detected, walk
                playerState.IsWalking = true;
                Debug.Log("OnWalk");
                OnWalk?.Invoke();
            }

            // Update the last tap time
            lastTapTime = Time.time;
            // Reset idle event
            isIdleEventTriggered = false;
        }
        else if (moveInputHorizontal == 0 && !isIdleEventTriggered)
        {
            // Key released, trigger idle
            Debug.Log("OnIdle");
            keyHeldDown = false;
            playerState.IsIdling = true;
            OnIdle?.Invoke();
            isIdleEventTriggered = true;
        }
    }


    private void FlipPlayerBasedOnDirection()
    {
        if (moveInput.x > 0)
        {
            // Moving right, ensure player is facing right
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            playerState.IsFacingRight = true;
        }
        else if (moveInput.x < 0)
        {
            // Moving left, flip the player's x scale to face left
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            playerState.IsFacingRight = false;
        }
    }

    private void DetectWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            WallDetector.position,
            BoxcastSize,
            0f,
            Vector2.right,
            0f,
            WallLayer);

        playerState.IsTouchingWall = hit.collider != null;

        // trigger touching the wall event
        if ( (playerState.IsTouchingWall && !isWallTouchEventTriggered))
        {
            isWallTouchEventTriggered = true;
            Debug.Log("OnWallTouch");
            OnWallTouch?.Invoke();
        }
        else if (!playerState.IsTouchingWall)
        {
            isWallTouchEventTriggered = false;
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
        Gizmos.color = playerState.IsTouchingWall ? Color.red : Color.green;
        Gizmos.DrawWireCube(WallDetector.position, BoxcastSize);
    }
}
