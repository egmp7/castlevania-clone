using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using egmp7.Game.Manager;

namespace Enemy.AI
{
    public class AddScore : EnemyAction
    {
        public SharedInt score = 100;

        private bool _completed = false;

        public override TaskStatus OnUpdate()
        {
            if (!_completed) { 
                if (MainManager.Instance == null) return TaskStatus.Failure;
                MainManager.Instance.score += score.Value;
                _completed = true;
            }
            return TaskStatus.Success;
        }
    }   
}