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
               input.RigidBody.velocity.x,
               input.direction,
               input.runSpeed,
               input.accelerationRate
               );

            // Preserve the vertical velocity and update horizontal speed
            input.RigidBody.velocity =
                new Vector2(
                    currentSpeed,
                    input.RigidBody.velocity.y);
            #endregion

            input.Animator.Play("Run");
        }
    }

}
