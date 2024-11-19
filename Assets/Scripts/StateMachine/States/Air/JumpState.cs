
using UnityEngine;

namespace Player.StateManagement
{

    public class JumpState : AirState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            #region Jump Logic
            // jump
            input.RigidBody.velocity =
                new Vector2(
                    input.RigidBody.velocity.x,
                    input.jumpForce);

            #endregion
            input.Animator.Play("Jump");
        }
    }
}
