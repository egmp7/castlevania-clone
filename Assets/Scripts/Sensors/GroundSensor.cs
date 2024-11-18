using UnityEngine;

namespace Game.Sensors
{
    public class GroundSensor : GameSensor
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ground"))
            {
                sensorState = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ground"))
            {
                sensorState = false;
            }
        }
    }
}