using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private enum AnimationState
    {
        Idle,
        Walk,
        Run,
        Jump,
        Fall,
    }

    private Animator animator;
    private AnimationState currentAnimationState = AnimationState.Idle;
    private AnimationState newAnimationState;
    private bool isRunning;
    private bool isWalking;
    private bool isJumping;

    private void OnEnable()
    {
        // Single Triggers
        PlayerMovementHorizontal.OnPlayerRun += OnPlayerRun;
        PlayerMovementHorizontal.OnPlayerWalk += OnPlayerWalk;

        // Loop Triggers
        PlayerMovementHorizontal.OnPlayerIdle += OnPlayerIdle;
        PlayerMovementVertical.OnPlayerFalling += OnPlayerFalling;
        PlayerMovementVertical.OnPlayerAscending += OnPlayerAscending;
        PlayerMovementVertical.OnPlayerGrounded += OnPlayerGrounded;
    }
    private void OnDisable()
    {
        // Single Triggers
        PlayerMovementHorizontal.OnPlayerRun -= OnPlayerRun;
        PlayerMovementHorizontal.OnPlayerWalk -= OnPlayerWalk;

        // Loop Triggers
        PlayerMovementHorizontal.OnPlayerIdle -= OnPlayerIdle;
        PlayerMovementVertical.OnPlayerFalling -= OnPlayerFalling;
        PlayerMovementVertical.OnPlayerAscending -= OnPlayerAscending;
        PlayerMovementVertical.OnPlayerGrounded -= OnPlayerGrounded;
    }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
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
                case AnimationState.Fall:
                    animator.Play("JumpFall");
                    break;
            }
            currentAnimationState = newAnimationState; // Update the current animation state
        }
    }

    private void OnPlayerRun()
    {
        if (!isJumping)
        {
            isRunning = true;
            isWalking = false;
            newAnimationState = AnimationState.Run;
        }
    }

    private void OnPlayerWalk()
    {
        if (!isJumping)
        {
            isRunning = false;
            isWalking = true;
            newAnimationState = AnimationState.Walk;
        }
    }

    private void OnPlayerIdle()
    {
        if (!isJumping)
        {
            isRunning = false;
            isWalking = false;
            newAnimationState = AnimationState.Idle;
        }
    }

    private void OnPlayerGrounded()
    {
        isJumping = false;

        if (isRunning)
        {
            newAnimationState = AnimationState.Run;
        }
        else if (isWalking)
        {
            newAnimationState = AnimationState.Walk;
        }
        else
        {
            newAnimationState = AnimationState.Idle;
        }
    }

    private void OnPlayerFalling()
    {
        newAnimationState = AnimationState.Fall;
    }

    private void OnPlayerAscending()
    {
        isJumping = true;
        newAnimationState = AnimationState.Jump;
    }
}
