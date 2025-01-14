using BehaviorDesigner.Runtime.Tasks;
using egmp7.Game.Manager;

namespace Enemy.AI
{
    public class AddScore : EnemyAction
    {
        public int score = 100;

        private bool _completed = false;

        public override TaskStatus OnUpdate()
        {
            if (!_completed) { 
                MainManager.Instance.score += score;
                _completed = true;
            }
            return TaskStatus.Success;
        }
    }   
}