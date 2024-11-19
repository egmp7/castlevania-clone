using UnityEngine;

namespace Game.Sensors
{
    public class PlayerSensor : GameSensor
    {
        private void Awake()
        {
            _sensorTag = "Player";
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_sensorTag))
            {
                _sensorState = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(_sensorTag))
            {
                _sensorState = false;
            }
        }
    }
}