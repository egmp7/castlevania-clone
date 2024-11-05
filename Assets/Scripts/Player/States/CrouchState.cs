using UnityEngine;

namespace Player.StateManagement
{

    public class CrouchState : GroundState
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            #region Stop Moving
            input.rigidBody.velocity =
                new Vector2(
                    0,
                    input.rigidBody.velocity.y);
            #endregion
            input.animator.Play("Crouch");
        }
    }
}

