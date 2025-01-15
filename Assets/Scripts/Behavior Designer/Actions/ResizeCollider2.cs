using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class ResizeCollider2 : EnemyAction
    {
        // Direct reference to the collider
        private BoxCollider2D collider2D;

        // New size for the collider
        public Vector2 newSize;

        public override TaskStatus OnUpdate()
        {
            if (collider2D == null)
            {
                ErrorManager.LogMissingComponent<BoxCollider2D>(gameObject);
                return TaskStatus.Failure;
            }

            // Resize the collider
            collider2D.size = newSize;

            return TaskStatus.Success;
        }
    }
}

