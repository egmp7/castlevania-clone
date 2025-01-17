using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.AI
{
    public class TestConditional : Conditional
    {
        public bool Test;

        public override TaskStatus OnUpdate()
        {
            if ( Test)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}