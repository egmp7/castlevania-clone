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
    private bool isRunning;
    private bool isWalking;
    private bool isPlayerJumping;
    private bool isPlayerOnGround;
    private bool animationEnded;

    private void OnEnable()
    {
        // Single Triggers
        PlayerMovementHorizontal.OnIdle += PlayIdleAnimation;
        PlayerMovementHorizontal.OnRun += PlayRunAnimation;
        PlayerMovementHorizontal.OnWalk += PlayWalkAnimation;
        PlayerMovementHorizontal.OnWallTouch += OnWallTouch;

        PlayerMovementVertical.OnGround += OnPlayerGrounded;
        PlayerMovementVertical.OnFall += PlayFallAnimation;
        PlayerMovementVertical.OnJump += PlayJumpAnimation;

        PlayerLedgeClimb.OnLedgeClimb += OnLedgeClimb;
        PlayerLedgeClimb.OnLedgeHang += OnLedgeHang;
        PlayerLedgeClimb.OnLedgeRelease += OnLedgeRelease;
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
        PlayerLedgeClimb.OnLedgeRelease -= OnLedgeRelease;
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
    }

    private void OnAnimationEnd(AnimatorStateInfo stateInfo)
    {
        string ledgeClimbAnimation = animationClips[AnimationState.LedgeClimb];

        if (stateInfo.IsName(ledgeClimbAnimation))
        {
            Debug.Log("OnClimbAnimationEnded");
            OnClimbAnimationEnded?.Invoke();

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
            isPlayerOnGround = true;
            isPlayerJumping = false;
        }
    }

    private void PlayRunAnimation()
    {
        isRunning = true;
        isWalking = false;

        if (!isPlayerOnGround) return;
        newAnimationState = AnimationState.Run;
    }

    private void PlayWalkAnimation()
    {
        isRunning = false;
        isWalking = true;

        if (!isPlayerOnGround) return;
        newAnimationState = AnimationState.Walk;
    }

    private void PlayIdleAnimation()
    {
        isRunning = false;
        isWalking = false;

        if (!isPlayerOnGround) return;
        newAnimationState = AnimationState.Idle;
    }

    private void PlayFallAnimation()
    {
        isPlayerOnGround = false;
        newAnimationState = AnimationState.Fall;
    }

    private void PlayJumpAnimation() {
        isPlayerJumping = true;
        isPlayerOnGround = false;
        newAnimationState |= AnimationState.Jump;
    }

    private void OnPlayerGrounded()
    {
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
        isPlayerOnGround = true ;
        isPlayerJumping = false;
    }

    private void OnLedgeClimb()
    {
        newAnimationState = AnimationState.LedgeClimb;
    }

    private void OnWallTouch()
    {
        if (isPlayerJumping) return;

        newAnimationState = AnimationState.Idle;
    }

    private void OnLedgeHang()
    {
        //isHanging = true;
        newAnimationState = AnimationState.LedgeHang;
    }

    private void OnLedgeRelease()
    {
        //isHanging = false;
    }
}
