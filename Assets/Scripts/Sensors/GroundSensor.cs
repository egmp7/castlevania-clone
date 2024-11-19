using UnityEngine;

namespace Game.Sensors
{
    public class GroundSensor : GameSensor
    {

        private void Awake()
        {
            _sensorTag = "Ground";
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