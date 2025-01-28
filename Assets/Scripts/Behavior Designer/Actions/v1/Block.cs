using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


namespace Enemy.AI
{
    public class Block : EnemyAction
    {
        public SharedFloat blockChance;

        public override TaskStatus OnUpdate()
        {
            if (Random.value <= blockChance.Value)
            {
                _animator.Play("Block", -1, 0f);
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}