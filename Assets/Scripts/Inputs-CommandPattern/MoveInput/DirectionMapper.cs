using UnityEngine;

namespace InputCommands.Move
{

    public class DirectionMapper 
    {
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

        private readonly float idleRatio = 0.33f;

        public State Update(Vector2 moveInput)
        {
            #region Direction Mapper
            if (moveInput.y > idleRatio) // Top row
            {
                if (moveInput.x < -idleRatio) return State.UpLeft;
                else if (moveInput.x > idleRatio) return State.UpRight;
                else return State.Up;
            }
            else if (moveInput.y < -idleRatio) // Bottom row
            {
                if (moveInput.x < -idleRatio) return State.DownLeft;
                else if (moveInput.x > idleRatio) return State.DownRight;
                else return State.Down;
            }
            else // Middle row
            {
                if (moveInput.x < -idleRatio) return State.Left;
                else if (moveInput.x > idleRatio) return State.Right;
                else return State.Idle ;
            }
            #endregion
        }
    }
}