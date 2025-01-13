using UnityEngine;

namespace Game.Sensors
{
    public class CollisionLayerSensor : GameSensor
    {
        [SerializeField] private LayerMask layerMask; // The LayerMask to compare collisions against

        private void Start()
        {
            Debug.Log("HEllo world");   
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the collided object's layer matches the LayerMask
            if (IsInLayerMask(other.gameObject.layer, layerMask))
            {
                Debug.Log($"SENSOR On {other.gameObject.name}");
                _sensorState &= true;
            }
           
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Check if the collided object's layer matches the LayerMask
            if (IsInLayerMask(other.gameObject.layer, layerMask))
            {
                Debug.Log($"SENSOR Off {other.gameObject.name}");
                _sensorState &= false;
            }

        }

        /// <summary>
        /// Checks if a layer is included in a LayerMask.
        /// </summary>
        /// <param name="layer">The layer to check.</param>
        /// <param name="mask">The LayerMask to compare against.</param>
        /// <returns>True if the layer is included in the mask, otherwise false.</returns>
        private bool IsInLayerMask(int layer, LayerMask mask)
        {
            return (mask.value & (1 << layer)) != 0;
        }
    }
}

