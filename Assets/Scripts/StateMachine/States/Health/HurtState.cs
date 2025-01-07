using UnityEngine;

namespace Player.StateManagement
{
    public class HurtState : HealthState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            input.RigidBody.velocity = Vector2.zero;

            input.Animator.Play("Hurt");
        }
    }
}

