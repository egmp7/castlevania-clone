
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
            input.rigidBody.velocity =
                new Vector2(
                    input.rigidBody.velocity.x,
                    input.jumpForce);

            #endregion
            input.animator.Play("Jump");
        }
    }
}
