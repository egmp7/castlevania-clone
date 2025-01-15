using Game.Sensors;
using System.Linq;
using UnityEngine;

namespace egmp7.Game.Sensors
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionSensor : GameSensor
    {
        [Tooltip("The LayerMask to compare collisions against.")]
        [SerializeField] private LayerMask layerMask;

        [Tooltip("The tag to compare against (optional). Leave empty to ignore.")]
        [SerializeField] private string tagName;

        private void Awake()
        {
            ValidateSerializedFields();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsCollisionValid(other))
            {
                //Debug.Log($"SENSOR On {other.gameObject.name}");
                _sensorState = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsCollisionValid(other))
            {
                //Debug.Log($"SENSOR Off {other.gameObject.name}");
                _sensorState = false;
            }
        }

        /// <summary>
        /// Validates if the collision is valid based on the LayerMask and optional tag name.
        /// </summary>
        /// <param name="collider">The collider involved in the collision.</param>
        /// <returns>True if the collision is valid, otherwise false.</returns>
        private bool IsCollisionValid(Collider2D collider)
        {
            int layer = collider.gameObject.layer;

            // Validate layer against the LayerMask
            if (!IsInLayerMask(layer, layerMask))
                return false;

            // Validate tag against the tag name if specified
            if (!string.IsNullOrEmpty(tagName) && !collider.CompareTag(tagName))
                return false;

            return true;
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

        /// <summary>
        /// Validates the serialized fields and logs errors if invalid.
        /// </summary>
        private void ValidateSerializedFields()
        {
            if (layerMask == 0)
            {
                Debug.LogWarning($"{nameof(CollisionSensor)}: LayerMask is not set. This sensor will not detect any collisions.");
            }

            if (!string.IsNullOrEmpty(tagName) && !IsTagDefined(tagName))
            {
                Debug.LogError($"{nameof(CollisionSensor)}: Tag '{tagName}' does not exist in the project. Please check your input.");
            }
        }

        /// <summary>
        /// Checks if a tag is defined in the project.
        /// </summary>
        /// <param name="tag">The tag to check.</param>
        /// <returns>True if the tag is defined, otherwise false.</returns>
        private bool IsTagDefined(string tag)
        {
            try
            {
                return !string.IsNullOrEmpty(tag) && !string.IsNullOrWhiteSpace(tag) && UnityEditorInternal.InternalEditorUtility.tags.Contains(tag);
            }
            catch
            {
                Debug.LogError($"{nameof(CollisionSensor)}: Unable to validate the tag. This check may fail in builds.");
                return false;
            }
        }
    }
}
