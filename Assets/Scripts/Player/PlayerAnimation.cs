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
        LedgeClimb,
        LedgeHang,
    }

    private Animator animator;
    private AnimationState currentAnimationState = AnimationState.Idle;
    private AnimationState newAnimationState;
    private bool isRunning;
    private bool isWalking;
    private bool isJumping;
    private bool isHanging;

    private void OnEnable()
    {
        // Single Triggers
        PlayerMovementHorizontal.OnPlayerRun += OnPlayerRun;
        PlayerMovementHorizontal.OnPlayerWalk += OnPlayerWalk;
        PlayerLedgeClimb.OnLedgeClimbStart += OnLedgeClimbStart;
        PlayerLedgeClimb.OnLedgeHangStart += OnLedgeHangStart;
        PlayerLedgeClimb.OnLedgeClimbEnd += OnLedgeClimbEnd;
        PlayerLedgeClimb.OnLedgeReleaseEnd += OnLedgeReleaseEnd;

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
        PlayerLedgeClimb.OnLedgeClimbStart -= OnLedgeClimbStart;
        PlayerLedgeClimb.OnLedgeHangStart -= OnLedgeHangStart;
        PlayerLedgeClimb.OnLedgeClimbEnd -= OnLedgeClimbEnd;
        PlayerLedgeClimb.OnLedgeReleaseEnd -= OnLedgeReleaseEnd;

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
            Debug.Log(newAnimationState);
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
                case AnimationState.LedgeClimb:
                    animator.Play("LedgeClimb");
                    break;
                case AnimationState.LedgeHang:
                    animator.Play("LedgeHang");
                    break;
            }
            currentAnimationState = newAnimationState; // Update the current animation state
        }
    }

    private void OnPlayerRun()
    {
        if (isJumping) return;
        if (isHanging) return;

        isRunning = true;
        isWalking = false;
        newAnimationState = AnimationState.Run;
    }

    private void OnPlayerWalk()
    {
        if (isJumping) return;
        if (isHanging) return;

        isRunning = false;
        isWalking = true;
        newAnimationState = AnimationState.Walk;
    }

    private void OnPlayerIdle()
    {
        if (isJumping) return;
        if (isHanging) return;

        isRunning = false;
        isWalking = false;
        newAnimationState = AnimationState.Idle;

    }

    private void OnPlayerGrounded()
    {
        if (isHanging) return;

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
        if (isHanging) return;

        newAnimationState = AnimationState.Fall;
    }

    private void OnPlayerAscending()
    {
        isJumping = true;
        newAnimationState = AnimationState.Jump;
    }

    private void OnLedgeClimbStart()
    {
        newAnimationState = AnimationState.LedgeClimb;
    }

    private void OnLedgeHangStart()
    {
        isHanging = true;
        newAnimationState = AnimationState.LedgeHang;
    }

    private void OnLedgeReleaseEnd()
    {
        isHanging = false;
    }

    private void OnLedgeClimbEnd()
    {
        isHanging = false;
    }
}
