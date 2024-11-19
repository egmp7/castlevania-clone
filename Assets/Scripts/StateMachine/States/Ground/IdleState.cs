using UnityEngine;

namespace Player.StateManagement
{

    public class IdleState : GroundState
    {
        protected override void OnEnter()
        {
            base.OnEnter();

            #region Move Player Logic
            input.RigidBody.velocity =
                new Vector2(
                    0,
                    input.RigidBody.velocity.y);
            #endregion
            input.Animator.Play("Idle");
        }
    }

}