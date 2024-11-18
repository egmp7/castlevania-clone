using UnityEngine;

namespace Game.Sensors
{
    public class PlayerSensor : GameSensor
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                sensorState = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                sensorState = false;
            }
        }
    }
}