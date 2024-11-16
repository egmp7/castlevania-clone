using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemController : MonoBehaviour
{
    public static event Action OnAttack;
    public static event Action OnKick;

    // states
    [HideInInspector]
    public enum MoveState

    {
        Idle,
        Up,
        UpLeft,
        UpRight,
        Down,
        DownLeft,
        DownRight,
        Left,
        Right
    }

    [HideInInspector] public MoveState currentMoveState;
    [HideInInspector] public bool isDoubleTap;

    [Tooltip("How big the idle area is")]
    [SerializeField][Range(0.1f, 0.8f)] float IdleRatio = 0.33f;

    [Tooltip("Threshold for double tap conmtrol")]
    [Range(0.05f, 1.0f)] public float doubleTapThreshold = 0.3f;

    private readonly Stack<MoveState> stateStack = new();
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction kickAction;
    private Vector2 moveInput;
    private bool isTapCheck;
    private float lastTapTime;

    private bool isAttackPressed;
    private bool isKickPressed;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("ActionA");
        kickAction = InputSystem.actions.FindAction("ActionB");
    }

    private void Start()
    {
        ChangeState(MoveState.Idle);
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        HandleInput();
        CheckDoubleTap();
        UpdateState();
        CheckActionEvent(attackAction, ref isAttackPressed, OnAttack);
        CheckActionEvent(kickAction, ref isKickPressed, OnKick);
    }

    private void CheckActionEvent(InputAction action, ref bool isPressed, Action onEvent)
    {
        if (action.IsPressed() && !isPressed)
        {
            isPressed = true;
            onEvent?.Invoke();
        }
        else if (!action.IsPressed())
        {
            isPressed = false;
        }
    }

    public void ChangeState(MoveState newState)
    {
        if (currentMoveState == newState) return;
        currentMoveState = newState;
    }

    private void UpdateState()
    {
        if (stateStack.Count == 0)
        {
            ChangeState(MoveState.Idle);
        }
        else
        {
            ChangeState(stateStack.Peek());
        }
    }

    private void CheckDoubleTap()
    {
        if (moveInput.x != 0 && !isTapCheck)
        {
            isTapCheck = true;

            // Check for double tap for running
            if (Time.time - lastTapTime < doubleTapThreshold)
            {
                isDoubleTap = true;
            }
            else
            {
                isDoubleTap = false;
            }

            lastTapTime = Time.time;
        }

        else if (moveInput.x == 0)
        {
            isTapCheck = false;
            isDoubleTap = false;
        }
    }

    private void HandleInput()
    {
        if (moveInput.y > IdleRatio) // Top row
        {
            if (moveInput.x < -IdleRatio) stateStack.Push(MoveState.UpLeft);
            else if (moveInput.x > IdleRatio) stateStack.Push(MoveState.UpRight);
            else stateStack.Push(MoveState.Up);
        }
        else if (moveInput.y < -IdleRatio) // Bottom row
        {
            if (moveInput.x < -IdleRatio) stateStack.Push(MoveState.DownLeft);
            else if (moveInput.x > IdleRatio) stateStack.Push(MoveState.DownRight);
            else stateStack.Push(MoveState.Down);
        }
        else // Middle row
        {
            if (moveInput.x < -IdleRatio) stateStack.Push(MoveState.Left);
            else if (moveInput.x > IdleRatio) stateStack.Push(MoveState.Right);
            else stateStack.Clear(); ;
        }
    }
}
