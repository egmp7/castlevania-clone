using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class ResizeCollider : EnemyAction
    {
        // Reference to the Game Object
        public SharedGameObject target;

        // New size for the collider
        public SharedVector2 newSize;

        private BoxCollider2D _collider2D;

        public override void OnAwake()
        {
            _collider2D = target.Value.GetComponent<BoxCollider2D>();
        }

        public override TaskStatus OnUpdate()
        {
            if (_collider2D == null)
            {
                ErrorManager.LogMissingComponent<BoxCollider2D>(target.Value);
                return TaskStatus.Failure;
            }

            // Resize the collider
            _collider2D.size = newSize.Value;
            return TaskStatus.Success;
        }
    }
}

