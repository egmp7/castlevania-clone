using Player.StateManagement;
using UnityEngine;

namespace AnimationEvents
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        private StateMachine stateMachine;

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
        }

        public void OnAnimationEnd()
        {
            stateMachine.Idle();
        }

        public void OnAttackAnimation()
        {
            Vector2 localOffset;
            float attackRadius;
            State currentState = stateMachine.GetCurrentState();

            if (currentState is AttackState attackState)
            {
                localOffset = attackState.GetLocalOffset();
                attackRadius = attackState.GetAttackRadius();

                // Guards
                if (localOffset == null)
                    Debug.LogError("LocalOffset is null!");
                if (stateMachine.EnemyDetectorPosition == null)
                    Debug.LogError("EnemyDetectorPosition is null!");
                if (stateMachine.EnemyLayer.value == 0)
                    Debug.LogError("EnemyLayer has not been set!");
                
                // Attack Position
                Vector2 attackPosition =
                new(stateMachine.transform.position.x + (stateMachine.attackOffset.x + localOffset.x) * stateMachine.direction,
                         stateMachine.transform.position.y + stateMachine.attackOffset.y + localOffset.y);

                // Generate Hit
                Collider2D hit = Physics2D.OverlapCircle(
                    attackPosition,
                    attackRadius,
                    stateMachine.EnemyLayer);

                // debug Hit
                if (stateMachine.debugDraw)
                    Utilities.DrawCircleBounds(
                        attackPosition,
                        attackRadius,
                        Color.red);

                if (hit != null)
                {
                    Debug.Log("Enemy Hit");

                    // if hit enemy
                    //if (hit.TryGetComponent<EnemyHP>(out var enemyHP))
                    //{
                    //    enemyHP.TakeDamage(_damageValue);
                    //    Debug.Log("Enemy Touched");
                    //}
                    //else
                    //{
                    //    Debug.LogError("No EnemyHP component found on hit object!");
                    //}
                }
            }
        }
    }
}

