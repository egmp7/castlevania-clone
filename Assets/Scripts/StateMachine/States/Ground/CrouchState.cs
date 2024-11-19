using UnityEngine;

namespace Player.StateManagement
{

    public class CrouchState : GroundState
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            #region Stop Moving
            input.RigidBody.velocity =
                new Vector2(
                    0,
                    input.RigidBody.velocity.y);
            #endregion
            input.Animator.Play("Crouch");
        }
    }
}

