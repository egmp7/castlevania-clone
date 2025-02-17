using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using egmp7.SO;
using UnityEngine;

namespace egmp7.BehaviorDesigner.Enemy
{
    [TaskIcon("Assets/Art/customAction.png")]
    [TaskCategory("egmp7")]
    [TaskDescription("Test if EnemyHurtSO sharedObject is initialized")]
    public class InitializedSharedObjectTest : EnemyAction
    {
        public SharedObject myObject;

        private EnemyHurtSO _enemyHurtSO;

        public override void OnStart()
        {
            if (myObject == null)
            {
                Debug.LogError("myObject is null!");
                return;
            }

            if (myObject.Value == null)
            {
                Debug.LogError("myObject.Value is null!");
                return;
            }

            _enemyHurtSO = myObject.Value as EnemyHurtSO;

            if (_enemyHurtSO == null)
            {
                Debug.LogError("myObject.Value is not of type AttackSO!");
            }

            Debug.Log($"Initialized: {_enemyHurtSO}");
        }


        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}
