using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimationController : MonoBehaviour
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
        Crouch,
        Attack,
    }

    private readonly Dictionary<AnimationState, string> animationClips = new()
    {
        { AnimationState.Idle, "Idle" },
        { AnimationState.Walk, "Walk" },
        { AnimationState.Run, "Run" },
        { AnimationState.Jump, "Jump" },
        { AnimationState.Fall, "Fall" },
        { AnimationState.LedgeClimb, "LedgeClimb" },
        { AnimationState.LedgeHang, "LedgeHang" },
        { AnimationState.Crouch, "Crouch" },
        { AnimationState.Attack, "Punch01" },
    };

    private Animator animator;
    private AnimationState currentAnimationState = AnimationState.Idle;
    private AnimationState newAnimationState;
    private bool animationEnded;

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

    public void PlayIdleAnimation()
    {
        newAnimationState = AnimationState.Idle;
    }

    public void PlayRunAnimation()
    {
        newAnimationState = AnimationState.Run;
    }

    public void PlayWalkAnimation()
    {
        newAnimationState = AnimationState.Walk;
    }

    public void PlayFallAnimation()
    {
        newAnimationState = AnimationState.Fall;
    }

    public void PlayJumpAnimation() {
        newAnimationState |= AnimationState.Jump;
    }

    public void PlayLedgeClimbAnimation()
    {
        newAnimationState = AnimationState.LedgeClimb;
    }

    public void PlayLedgeHangAnimation()
    {
        newAnimationState = AnimationState.LedgeHang;
    }

    public void PlayCrouchAnimation()
    {
        newAnimationState = AnimationState.Crouch;
    }

    public void PlayAttackAnimation()
    {
        newAnimationState = AnimationState.Attack;
    }
}
