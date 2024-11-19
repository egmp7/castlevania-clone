using Legacy.TestEnemy;
using UnityEngine;

namespace Player.StateManagement
{

    public abstract class AttackState : State
    {
        protected float _damageValue;
        protected float _attackRadius;
        protected Vector2 _localOfsset;

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

        public void AnimationAttack()
        {
            // Guards
            if (_localOfsset == null)
                Debug.LogError("LocalOffset is null!");
            if (input.EnemyDetectorPosition == null)
                Debug.LogError("EnemyDetectorPosition is null!");
            if (input.EnemyLayer.value == 0)
                Debug.LogError("EnemyLayer has not been set!");

            Vector2 attackPosition = 
                new (input.transform.position.x + (input.attackOffset.x + _localOfsset.x) * input.direction, 
                     input.transform.position.y + input.attackOffset.y + _localOfsset.y);

            // Hit
            Collider2D hit = Physics2D.OverlapCircle(
                attackPosition,
                _attackRadius,
                input.EnemyLayer);

            // debug Hit
            if (input.debugDraw)
            Utilities.DrawCircleBounds(
                attackPosition, 
                _attackRadius, 
                Color.red);

            if (hit != null)
            {
                // if hit enemy
                if (hit.TryGetComponent<EnemyHP>(out var enemyHP))
                {
                    enemyHP.TakeDamage(_damageValue);
                    Debug.Log("Enemy Touched");
                }
                else
                {
                    Debug.LogError("No EnemyHP component found on hit object!");
                }
            }
        }
    }
}
