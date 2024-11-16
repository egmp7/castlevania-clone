namespace InputCommands.Move
{

    public class DirectionDetector
    {
        private int _direction;

        public int Update(DirectionMapper.State state)
        {
            #region DoubleTapDetector

            if (state == DirectionMapper.State.Right ||
                state == DirectionMapper.State.UpRight ||
                state == DirectionMapper.State.DownRight)
            {
                _direction = 1;
            }
            else if (state == DirectionMapper.State.Left ||
                state == DirectionMapper.State.UpLeft ||
                state == DirectionMapper.State.DownLeft)
            {
                _direction = - 1;
            }
            else
            {
                _direction = 0;
            }

            return _direction;
            
            #endregion
        }

        public int GetDirection()
        {
            return _direction;
        }
    }
}