using UnityEngine;

namespace Player.StateManagement
{

    public class WalkState : GroundState
    {
        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            #region Move Player Logic
            // Lerp for smooth acceleration
            float currentSpeed = Utilities.CalculateCurrentSpeed(
                input.RigidBody.velocity.x,
                input.direction,
                input.walkSpeed,
                input.accelerationRate
                );

            // Preserve the vertical velocity and update horizontal speed
            input.RigidBody.velocity =
                new Vector2(
                    currentSpeed,
                    input.RigidBody.velocity.y);

            #endregion
            input.Animator.Play("Walk");
        }
    }
}
