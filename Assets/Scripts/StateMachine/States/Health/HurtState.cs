using UnityEngine;

namespace Player.StateManagement
{
    public class HurtState : HealthState
    {
        protected override void OnEnter()
        {
            base.OnEnter();
            input.RigidBody.velocity = Vector2.zero;
            //input.healthManager.TakeDamage(input.damageListener.GetCurrentDamage());
            input.Animator.Play("Hurt");
        }
    }
}

