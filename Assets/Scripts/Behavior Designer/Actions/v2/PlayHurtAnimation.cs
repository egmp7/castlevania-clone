using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using egmp7.SO;
using UnityEngine;

namespace egmp7.BehaviorDesigner.Enemy
{
    [TaskIcon("Assets/Art/customAction.png")]
    [TaskCategory("egmp7")]
    [TaskDescription("Plays an animation from an EnemyHurtSO")]
    public class PlayHurtAnimation : EnemyAction
    {
        public SharedObject EnemyHurtSO;

        private EnemyHurtSO _enemyHurtSO;

        public override void OnStart()
        {
            InitEnemyHurtSO();
            _animator.Play(_enemyHurtSO.animation);
        }


        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        private void InitEnemyHurtSO()
        {
            if (EnemyHurtSO == null)
            {
                Debug.LogError("EnemyHurtSO is null!");
                return;
            }

            if (EnemyHurtSO.Value == null)
            {
                Debug.LogError("EnemyHurtSO.Value is null!");
                return;
            }

            _enemyHurtSO = EnemyHurtSO.Value as EnemyHurtSO;

            if (_enemyHurtSO == null)
            {
                Debug.LogError("EnemyHurtSO.Value is not of type EnemyHurtSO!");
            }

            Debug.Log($"Initialized: {_enemyHurtSO}");
        }
    }
}
