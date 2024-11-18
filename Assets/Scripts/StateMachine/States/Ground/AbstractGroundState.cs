using UnityEngine;

namespace Player.StateManagement
{

    public abstract class GroundState : State
    {
        protected override void OnUpdate()
        {
            base.OnUpdate();
            FlipPlayerBasedOnDirection();
        }

        private void FlipPlayerBasedOnDirection()
        {
            #region Flip Logic
            if (input.direction == -1)
            {
                // Moving left, flip the player's x scale to face left
                input.transform.localScale = new Vector3(
                    -input.originalScale.x, 
                    input.originalScale.y, 
                    input.originalScale.z);

            }
            else if (input.direction == 1)
            {
                // Moving right, ensure player is facing right
                input.transform.localScale = new Vector3(
                    input.originalScale.x, 
                    input.originalScale.y, 
                    input.originalScale.z);
            }
            #endregion
        }
    }

}
