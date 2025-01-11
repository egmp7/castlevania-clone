using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


namespace Enemy.AI
{
    public class Block : EnemyAction
    {
        public float blockChance = 0.8f;

        public override TaskStatus OnUpdate()
        {
            if (Random.value <= blockChance)
            {
                _animator.Play("Block", -1, 0f);
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;

        }


    }
}