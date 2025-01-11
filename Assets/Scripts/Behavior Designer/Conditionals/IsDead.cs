using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.AI
{
    public class IsDead : EnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            var currentHealth = _healthManager.GetCurrentHealth();
            if (currentHealth <= 0) return TaskStatus.Success;
            return TaskStatus.Failure;
        }
    }
}
