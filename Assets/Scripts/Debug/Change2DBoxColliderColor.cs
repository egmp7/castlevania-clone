using UnityEngine;

namespace egmp7.Debug.Collider
{

    //[ExecuteAlways]
    public class Collider2DVisualizer : MonoBehaviour
    {
        public Color colliderColor = Color.red; // Set your preferred color here

        private void OnDrawGizmos()
        {
            var collider = GetComponent<BoxCollider2D>();
            if (collider)
            {
                Gizmos.color = colliderColor;
                Gizmos.matrix = transform.localToWorldMatrix;

                // Draw the BoxCollider2D as a wire rectangle
                Vector3 colliderCenter = collider.offset;
                Vector3 colliderSize = new Vector3(collider.size.x, collider.size.y, 0);
                Gizmos.DrawWireCube(colliderCenter, colliderSize);
            }
        }
    }
}

