using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.AI
{
    public class AttackPlayer : EnemyAction
    {
        public float moveSpeed = 5f;

        public override TaskStatus OnUpdate()
        {
            // Play the "Run" animation
            animator.Play("Punch01");
            return TaskStatus.Success;
        }
    }
}