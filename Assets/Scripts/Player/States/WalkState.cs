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
            float currentSpeed = Mathf.Lerp(
                input.rigidBody.velocity.x,
                input.facing * input.walkSpeed,
                input.accelerationRate * Time.fixedDeltaTime);

            // Preserve the vertical velocity and update horizontal speed
            input.rigidBody.velocity =
                new Vector2(
                    currentSpeed,
                    input.rigidBody.velocity.y);

            #endregion
            input.animator.Play("Walk");
        }
    }
}
