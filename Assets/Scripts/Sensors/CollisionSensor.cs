using Game.Sensors;
using UnityEngine;

namespace egmp7.Game.Sensors
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionSensor : GameSensor
    {
        [Tooltip("The tag to compare against")]
        [SerializeField] private string tagName;

        private void Awake()
        {
            ValidateFields();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(tagName))
            {
                sensorState = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(tagName))
            {
                sensorState = false;
            }
        }

        private void ValidateFields()
        {
            if (string.IsNullOrEmpty(tagName))
            {
                Debug.LogError($"{nameof(CollisionSensor)} on {gameObject.name} has no tag assigned!");
            }
        }
    }

}
