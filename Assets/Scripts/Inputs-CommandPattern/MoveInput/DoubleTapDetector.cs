using UnityEngine;

namespace InputCommands.Move
{

    public class DoubleTapDetector 
    {
        private bool isDoubleTap;
        private bool isTapCheck;
        private float lastTapTime;
        
        private readonly float doubleTapThreshold = 0.3f;

        public bool Update(DirectionMapper.State state)
        {
            #region DoubleTapDetector

            if ((DirectionMapper.State.Left == state || DirectionMapper.State.Right == state) 
                && !isTapCheck)
            {
                isTapCheck = true;

                // Check for double tap for running
                if (Time.time - lastTapTime < doubleTapThreshold)
                {
                    isDoubleTap = true;
                }
                
                lastTapTime = Time.time;
            }

            else if (DirectionMapper.State.Idle == state)
            {
                isTapCheck = false;
                isDoubleTap = false;
            }
            
            return isDoubleTap;
            #endregion
        }
    }
}