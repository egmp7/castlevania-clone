using UnityEngine;

namespace Player.StateManagement
{

    public abstract class AttackState : State
    {
        protected float _attackAmount;
        protected float _attackRadius;
        protected Vector2 _attackOffset;
        protected string _attackFrom = "Player";
        protected string _attackTo = "Enemy";

        protected override void OnEnter()
        {
            base.OnEnter();

            #region Stop Moving
            input.RigidBody.velocity =
                new Vector2(
                    0,
                    input.RigidBody.velocity.y);
            #endregion
        }

        public Vector2 GetLocalOffset()
        {
            return _attackOffset;
        }

        public float GetAttackRadius()
        {
            return _attackRadius;
        }
    }
}
