using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using egmp7.Game.Sensors;

namespace egmp7.BehaviorDesigner.Enemy
{
    [TaskIcon("Assets/Art/egmp7/customAction.png")]
    [TaskCategory("egmp7")]
    [TaskDescription("Calculates ReactionForce with proximity scaling and controlled output.")]
    public class WithinSight : Conditional
    {
        public SharedGameObject target; 

        private CollisionSensor _collisionSensor;

        public override void OnAwake()
        {
            ValidateFields();
        }

        public override TaskStatus OnUpdate()
        {
            if (_collisionSensor == null) 
            { 
                return TaskStatus.Inactive; 
            }

            if(_collisionSensor.GetState())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        private void ValidateFields()
        {
            if (!target.Value.TryGetComponent(out _collisionSensor))
            {
                var warningContext = $"in {FriendlyName}";
                ErrorManager.LogMissingComponent<CollisionSensor>(gameObject, warningContext);
            }
        }
    }
}