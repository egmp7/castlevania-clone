using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using egmp7.Game.Sensors;
using System;

namespace Enemy.AI
{
    public class WithinSight : Conditional
    {
        public SharedGameObject target; 

        private CollisionSensor _collisionSensor;

        public override void OnAwake()
        {
            if (!target.Value.TryGetComponent(out _collisionSensor))
            {
                throw new Exception($"_collisionSensor not initialized for {target.Name}");
            }
        }

        public override TaskStatus OnUpdate()
        {
            if(_collisionSensor.GetState())
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}