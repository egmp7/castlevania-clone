using UnityEngine;

namespace Player.StateManagement
{

    public class BlockState : GroundState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            input.RigidBody.velocity = Vector2.zero;
            input.Animator.Play("Block Idle");
        }

        public void PlayBlockAnimation()
        {
            input.Animator.Play("Block");
        }
    }
}
