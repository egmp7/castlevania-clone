using UnityEngine;
using UnityEngine.InputSystem;

namespace InputCommands.Move
{

    public class DirectionMapper : MonoBehaviour
    {
        [SerializeField][Range(0f,1f)] private float idleRatio = 0.33f;

        public enum State

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

        private InputAction moveAction;
        private Vector2 moveInput;
        private State currentState;


        private void Awake()
        {
            moveAction = InputSystem.actions.FindAction("Move");
        }

        private void Update()
        {
            #region Direction Mapper

            moveInput = moveAction.ReadValue<Vector2>();

            if (moveInput.y > idleRatio) // Top row
            {
                if (moveInput.x < -idleRatio) currentState = State.UpLeft;
                else if (moveInput.x > idleRatio) currentState = State.UpRight;
                else currentState = State.Up;
            }
            else if (moveInput.y < -idleRatio) // Bottom row
            {
                if (moveInput.x < -idleRatio) currentState = State.DownLeft;
                else if (moveInput.x > idleRatio) currentState = State.DownRight;
                else currentState = State.Down;
            }
            else // Middle row
            {
                if (moveInput.x < -idleRatio) currentState = State.Left;
                else if (moveInput.x > idleRatio) currentState = State.Right;
                else currentState = State.Idle;
            }
            #endregion
        }

        public State GetState()
        {
            return currentState;
        }

        public int GetDirection()
        {
            if (currentState == State.Right ||
                currentState == State.UpRight ||
                currentState == State.DownRight)
            {
                return 1;
            }
            else if (
                currentState == State.Left ||
                currentState == State.UpLeft ||
                currentState == State.DownLeft)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}