namespace InputCommands.Move
{

    public class DirectionSwitch
    {
        private int _direction;

        public DirectionSwitch() 
        { 
            _direction = 1;
        }

        public bool Update(DirectionMapper.State state)
        {
            #region DirectionSwitchDetector

            // Determine the current direction based on the state.
            int currentDirection = 0; // Default value for neutral state

            if (state == DirectionMapper.State.Right ||
                state == DirectionMapper.State.UpRight ||
                state == DirectionMapper.State.DownRight)
            {
                currentDirection = 1;
            }
            else if (state == DirectionMapper.State.Left ||
                     state == DirectionMapper.State.UpLeft ||
                     state == DirectionMapper.State.DownLeft)
            {
                currentDirection = -1;
            }

            // Only update direction for values 1 or -1
            if (currentDirection != 0 && _direction != currentDirection)
            {
                _direction = currentDirection;
                return true; // Direction has changed
            }

            return false; // No change in direction or neutral state

            #endregion
        }


        public int GetDirection()
        {
            return _direction;
        }
    }
}