using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    public static event Action OnAnimationEnd;

    public enum AnimationState
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

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationState newState)
    {
        if (currentAnimationState == newState) return;

        currentAnimationState = newState;

        if (animationClips.TryGetValue(newState, out string animationName))
        {
            animator.Play(animationName);
        }
    }

    // Animation event method for marking the end of an animation
    public void AnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }
}
