using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimationController : MonoBehaviour
{
    public static event Action OnAnimationEnd;

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

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentAnimationState != newAnimationState)
        {
            currentAnimationState = newAnimationState; 
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

    public void AnimationEnd()
    {
        OnAnimationEnd?.Invoke();
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
