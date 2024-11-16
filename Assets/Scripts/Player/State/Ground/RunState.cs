using UnityEngine;

namespace Player.StateManagement
{

    public class RunState : GroundState
    {
        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            #region Move Player Logic

            float currentSpeed = Utilities.CalculateCurrentSpeed(
               input.rigidBody.velocity.x,
               input.directionMapper.GetDirection(),
               input.runSpeed,
               input.accelerationRate
               );

            // Preserve the vertical velocity and update horizontal speed
            input.rigidBody.velocity =
                new Vector2(
                    currentSpeed,
                    input.rigidBody.velocity.y);
            #endregion

            input.animator.Play("Run");
        }
    }

}
