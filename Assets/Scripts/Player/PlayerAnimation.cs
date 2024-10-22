using System.Collections.Generic;
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

    private Dictionary<AnimationState, string> animationClips = new Dictionary<AnimationState, string>()
    {
        { AnimationState.Idle, "Idle" },
        { AnimationState.Walk, "Walk" },
        { AnimationState.Run, "Run" },
        { AnimationState.Jump, "Jump" },
        { AnimationState.Fall, "JumpFall" },
        { AnimationState.LedgeClimb, "LedgeClimb" },
        { AnimationState.LedgeHang, "LedgeHang" }
    };

    private Animator animator;
    private AnimationState currentAnimationState = AnimationState.Idle;
    private AnimationState newAnimationState;
    private bool isRunning;
    private bool isWalking;
    private bool isJumping;
    private bool isHanging;
    private bool animationEnded;

    private void OnEnable()
    {
        // Single Triggers
        PlayerMovementHorizontal.OnPlayerRun += OnPlayerRun;
        PlayerMovementHorizontal.OnPlayerWalk += OnPlayerWalk;
        PlayerLedgeClimb.OnLedgeClimbStart += OnLedgeClimbStart;
        PlayerLedgeClimb.OnLedgeHangStart += OnLedgeHangStart;
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


    // Plays Animations depending on events, also checks when animations ends
    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Check if the animation has ended based on normalizedTime
        if (stateInfo.normalizedTime >= 1.0f && !animationEnded)
        {
            OnAnimationEnd(stateInfo); // Trigger callback when animation ends
            animationEnded = true;
        }

        // Reset the animationEnded flag when transitioning to a new animation
        if (currentAnimationState != newAnimationState)
        {
            animationEnded = false;
            currentAnimationState = newAnimationState; // Update current animation state
            PlayAnimation(newAnimationState);
        }
    }

    private void PlayAnimation(AnimationState state)
    {
        if (animationClips.TryGetValue(state, out string animationName))
        {
            animator.Play(animationName);
        }

        // Handle special cases
        if (state == AnimationState.Run)
        {
            isRunning = true;
        }
        else if (state == AnimationState.Walk)
        {
            isRunning = false;
        }
    }

    private void OnAnimationEnd(AnimatorStateInfo stateInfo)
    {
        string ledgeClimbAnimation = animationClips[AnimationState.LedgeClimb];

        if (stateInfo.IsName(ledgeClimbAnimation))
        {
            isHanging = false;
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
}
