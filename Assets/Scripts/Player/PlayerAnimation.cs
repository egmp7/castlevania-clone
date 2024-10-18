using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private enum AnimationState
    {
        Idle,
        Walk,
        Run,
        Jump
    }

    private InputAction moveAction;
    private Animator animator;
    private AnimationState currentAnimationState = AnimationState.Idle;
    private AnimationState newAnimationState;
    private Vector2 moveInput;
    private bool isJumping = false; // A flag to track if the player is jumping
    private bool isRunning;

    private void OnEnable()
    {
        PlayerBasicMovement.OnPlayerRun += SetRunAnimation;
        PlayerBasicMovement.OnPlayerWalk += SetWalkAnimation;
        PlayerBasicMovement.OnPlayerIdle += SetIdleAnimation;
        PlayerBasicJump.OnPlayerJump += SetJumpAnimation;
        PlayerBasicJump.OnPlayerGrounded += OnPlayerGrounded;
    }
    private void OnDisable()
    {
        PlayerBasicMovement.OnPlayerRun -= SetRunAnimation;
        PlayerBasicMovement.OnPlayerWalk -= SetWalkAnimation;
        PlayerBasicMovement.OnPlayerIdle -= SetIdleAnimation;
        PlayerBasicJump.OnPlayerJump -= SetJumpAnimation;
        PlayerBasicJump.OnPlayerGrounded -= OnPlayerGrounded;
    }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        // Only play the animation if the state has changed
        if (currentAnimationState != newAnimationState)
        {
            switch (newAnimationState)
            {
                case AnimationState.Idle:
                    animator.Play("Idle");
                    break;
                case AnimationState.Walk:
                    animator.Play("Walk");
                    isRunning = false;
                    break;
                case AnimationState.Run:
                    animator.Play("Run");
                    isRunning = true;
                    break;
                case AnimationState.Jump:
                    animator.Play("Jump");
                    break;
            }
            currentAnimationState = newAnimationState; // Update the current animation state
        }
    }

    private void SetRunAnimation()
    {
        // Check if the player is jumping. If so, do not override the jump animation.
        if (!isJumping)
        {
            
            newAnimationState = AnimationState.Run;
        }
    }

    private void SetWalkAnimation()
    {
        // Check if the player is jumping. If so, do not override the jump animation.
        if (!isJumping)
        {
            newAnimationState = AnimationState.Walk;
        }
    }

    private void SetIdleAnimation()
    {
        // Check if the player is jumping. If so, do not override the jump animation.
        if (!isJumping)
        {
            newAnimationState = AnimationState.Idle;
        }
    }

    private void SetJumpAnimation()
    {
        Debug.Log("Jump Animation");
        // Set jump flag to true so other states can't override it
        isJumping = true;
        newAnimationState = AnimationState.Jump;
    }

    // This method is called when the player lands after jumping
    private void OnPlayerGrounded()
    {
        Debug.Log("Player Grounded");
        // Reset the jump flag so other states can override it
        isJumping = false;

        moveInput = moveAction.ReadValue<Vector2>();

        if(moveInput.x != 0)
        {
            if (isRunning)
            {
                newAnimationState = AnimationState.Run;
            }
            else
            {
                newAnimationState = AnimationState.Walk;
            }
        }
        else
        {
            // If no movement input is pressed, set the state to idle
            newAnimationState = AnimationState.Idle;
        }
    }
}
