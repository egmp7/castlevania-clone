using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static event Action OnClimbAnimationEnded;

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
        { AnimationState.Fall, "Fall" },
        { AnimationState.LedgeClimb, "LedgeClimb" },
        { AnimationState.LedgeHang, "LedgeHang" }
    };

    private Animator animator;
    private AnimationState currentAnimationState = AnimationState.Idle;
    private AnimationState newAnimationState;
    private PlayerState playerState;
    private bool animationEnded;

    private void OnEnable()
    {
        PlayerMovementHorizontal.OnIdle += PlayIdleAnimation;
        PlayerMovementHorizontal.OnRun += PlayRunAnimation;
        PlayerMovementHorizontal.OnWalk += PlayWalkAnimation;
        PlayerMovementHorizontal.OnWallTouch += OnWallTouch;

        PlayerMovementVertical.OnGround += OnPlayerGrounded;
        PlayerMovementVertical.OnFall += PlayFallAnimation;
        PlayerMovementVertical.OnJump += PlayJumpAnimation;

        PlayerLedgeClimb.OnLedgeClimb += OnLedgeClimb;
        PlayerLedgeClimb.OnLedgeHang += OnLedgeHang;
    }
    private void OnDisable()
    {
        PlayerMovementHorizontal.OnIdle -= PlayIdleAnimation;
        PlayerMovementHorizontal.OnRun -= PlayRunAnimation;
        PlayerMovementHorizontal.OnWalk -= PlayWalkAnimation;
        PlayerMovementHorizontal.OnWallTouch -= OnWallTouch;

        PlayerMovementVertical.OnGround -= OnPlayerGrounded;
        PlayerMovementVertical.OnFall -= PlayFallAnimation;
        PlayerMovementVertical.OnJump -= PlayJumpAnimation;

        PlayerLedgeClimb.OnLedgeClimb -= OnLedgeClimb;
        PlayerLedgeClimb.OnLedgeHang -= OnLedgeHang;
    }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
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
    }

    private void OnAnimationEnd(AnimatorStateInfo stateInfo)
    {
        // ledge climb animation
        if (stateInfo.IsName(animationClips[AnimationState.LedgeClimb]))
        {
            Debug.Log("OnClimbAnimationEnded");
            OnClimbAnimationEnded?.Invoke();
        }
    }

    private void PlayRunAnimation()
    {
        if (!playerState.IsOnGround) return;
        newAnimationState = AnimationState.Run;
    }

    private void PlayWalkAnimation()
    {
        if (!playerState.IsOnGround) return;
        newAnimationState = AnimationState.Walk;
    }

    private void PlayIdleAnimation()
    {
        if (!playerState.IsOnGround) return;
        newAnimationState = AnimationState.Idle;
    }

    private void PlayFallAnimation()
    {
        newAnimationState = AnimationState.Fall;
    }

    private void PlayJumpAnimation() {
        newAnimationState |= AnimationState.Jump;
    }

    private void OnPlayerGrounded()
    {
        if (playerState.IsIdleing)
        {
            newAnimationState = AnimationState.Idle;
        }
        if (playerState.IsWalking)
        {
            newAnimationState = AnimationState.Walk;
        }
        if (playerState.IsRunning)
        {
            newAnimationState = AnimationState.Run;
        }
    }

    private void OnLedgeClimb()
    {
        newAnimationState = AnimationState.LedgeClimb;
    }

    private void OnWallTouch()
    {
        if (playerState.IsJumping) return;
        newAnimationState = AnimationState.Idle;
    }

    private void OnLedgeHang()
    {
        newAnimationState = AnimationState.LedgeHang;
    }
}
