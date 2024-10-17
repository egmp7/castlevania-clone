using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField][Range(1.0f, 10.0f)] float WalkSpeed = 2.0f;
    [SerializeField][Range(1.0f, 10.0f)] float RunSpeed = 5.0f;
    [SerializeField][Range(0.05f, 1.0f)] float doubleTapThreshold = 0.3f;

    private InputAction moveAction;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isRunning;
    private float lastTapTime;
    private bool keyHeldDown;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
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
        moveInput = moveAction.ReadValue<Vector2>();

        if (moveInput.x != 0 && !keyHeldDown)
        {
            keyHeldDown = true;

            if (Time.time - lastTapTime < doubleTapThreshold)
            {
                // Double tap detected, start running
                isRunning = true;
            }
            else
            {
                // Single tap detected, walk
                isRunning = false;
            }

            // Update the last tap time
            lastTapTime = Time.time;
        }
        else if (moveInput.x == 0)
        {
            // Key released, ready for the next tap
            keyHeldDown = false;
        }
    }
}
