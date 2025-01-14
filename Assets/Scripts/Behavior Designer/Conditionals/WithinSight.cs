using BehaviorDesigner.Runtime.Tasks;
using egmp7.Game.Sensors;

namespace Enemy.AI
{
    public class WithinSight : Conditional
    {
        public CollisionLayerSensor sensor;

        public override void OnAwake()
        {
            if (sensor == null)
            {
                ErrorManager.LogMissingComponent<CollisionLayerSensor>(this);
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (sensor.GetState()) return TaskStatus.Success;
            return TaskStatus.Failure;
        }
    }
}