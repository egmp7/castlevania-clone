using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [HideInInspector] public enum MoveState
    
    {
        Idle,
        Walk,
        Run,
        Jump
    }

    [HideInInspector] public MoveState currentMoveState;
    [HideInInspector] public Vector2 moveInput;

    [Range(0.05f, 1.0f)] public float doubleTapThreshold = 0.3f;

    private Stack<MoveState> moveStateStack = new Stack<MoveState>();
    private InputAction moveAction;
    private bool keyHeldDown;
    private float lastTapTime;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        HandleInput();
    }

    private void HandleInput()
    {
        // If upward movement is detected (jump key pressed)
        if (moveInput.y > 0)
        {
            // Push the current state to stack if jumping
            if (currentMoveState != MoveState.Jump)
            {
                moveStateStack.Push(currentMoveState);
                currentMoveState = MoveState.Jump;
            }
        }
        // Horizontal movement logic
        else if (moveInput.x != 0 && !keyHeldDown)
        {
            keyHeldDown = true;

            // Check for double tap for running
            if (Time.time - lastTapTime < doubleTapThreshold)
            {
                currentMoveState = MoveState.Run;
            }
            else
            {
                currentMoveState = MoveState.Walk;
            }

            lastTapTime = Time.time;
        }

        else if (moveInput.x ==0)
        {
            keyHeldDown = false;
        }

        else if (currentMoveState == MoveState.Jump && moveInput.y == 0)
        {
            if (moveStateStack.Count > 0)
            {
                currentMoveState = moveStateStack.Pop();
            }
            else if (moveStateStack.Count == 0)
            {
                currentMoveState = MoveState.Idle;  // Default to Idle if stack is empty
            }
        }

        else if (moveInput == Vector2.zero)
        {
            moveStateStack.Clear();
            currentMoveState = MoveState.Idle;
        }
    }
}
