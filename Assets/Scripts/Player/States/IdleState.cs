using UnityEngine;

namespace Player.StateManagement
{

    public class IdleState : GroundState
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            #region Move Player Logic
            input.rigidBody.velocity =
                new Vector2(
                    0,
                    input.rigidBody.velocity.y);
            #endregion
            input.animator.Play("Idle");
        }
    }

}