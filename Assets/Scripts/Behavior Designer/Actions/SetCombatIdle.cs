using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.AI
{
    public class SetCombatIdle : EnemyAction
    {
        public SharedBool isCombatIdle;

        public override TaskStatus OnUpdate()
        {
            // reset firstCombat Idle
            isCombatIdle.SetValue(true);
            return TaskStatus.Success;
        }
    }
}