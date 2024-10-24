using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHorizontal : MonoBehaviour
{
    public static event Action OnPlayerRun;
    public static event Action OnPlayerWalk;
    public static event Action OnPlayerIdle;

    [Header("Movement")]
    [SerializeField][Range(1.0f, 10.0f)] float WalkSpeed = 2.0f;
    [SerializeField][Range(1.0f, 10.0f)] float RunSpeed = 5.0f;
    [SerializeField][Range(0.05f, 1.0f)] float doubleTapThreshold = 0.3f;

    private enum AnimationState
    {
        Idle,
        Walk,
        Run
    }

    private InputAction moveAction;
    private Rigidbody2D rb;
    private Vector3 originalScale; // Store the original scale
    private Vector2 moveInput;
    private float lastTapTime;
    private bool isRunning;
    private bool keyHeldDown;
    private bool isInputActive = true;

    private void OnEnable()
    {
        PlayerLedgeClimb.OnLedgeHangStart += TurnOffInput;
        PlayerLedgeClimb.OnLedgeClimbEnd += TurnOnInput;
        PlayerLedgeClimb.OnLedgeReleaseEnd += TurnOnInput;
    }
    private void OnDisable()
    {
        PlayerLedgeClimb.OnLedgeHangStart -= TurnOffInput;
        PlayerLedgeClimb.OnLedgeClimbEnd -= TurnOnInput;
        PlayerLedgeClimb.OnLedgeReleaseEnd -= TurnOnInput;
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // Get the initial scale
    }

    private void Update()
    {
        HandleInput();
        FlipPlayerBasedOnDirection(); // Flip the player based on direction
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float targetSpeed = isRunning ? RunSpeed : WalkSpeed;
        Vector2 movement = new Vector2(moveInput.x * targetSpeed, rb.velocity.y);
        rb.velocity = movement;
    }

    private void HandleInput()
    {
        if (!isInputActive) return;

        // Read Input
        moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.x != 0 && !keyHeldDown)
        {
            keyHeldDown = true;

            if (Time.time - lastTapTime < doubleTapThreshold)
            {
                // Double tap detected, start running
                isRunning = true;
                OnPlayerRun?.Invoke();
            }
            else
            {
                // Single tap detected, walk
                isRunning = false;
                OnPlayerWalk?.Invoke();
            }

            // Update the last tap time
            lastTapTime = Time.time;
        }
        else if (moveInput.x == 0)
        {
            // Key released, ready for the next tap
            keyHeldDown = false;
            OnPlayerIdle?.Invoke();

        }
    }

    private void FlipPlayerBasedOnDirection()
    {
        if (moveInput.x > 0)
        {
            // Moving right, ensure player is facing right
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        else if (moveInput.x < 0)
        {
            // Moving left, flip the player's x scale to face left
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
    }

    private void TurnOnInput()
    {
        isInputActive = true;
    }

    private void TurnOffInput()
    {
        isInputActive = false;
    }
}
