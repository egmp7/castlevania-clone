using BehaviorDesigner.Runtime.Tasks;
using Game.Sensors;

namespace Enemy.AI
{
    public class WithinSight : Conditional
    {
        public PlayerSensor playerSensor;

        public override TaskStatus OnUpdate()
        {
            if (playerSensor.GetState()) return TaskStatus.Success;
            return TaskStatus.Failure;
        }
    }
}