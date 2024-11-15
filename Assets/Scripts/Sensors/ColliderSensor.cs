using UnityEngine;

namespace Game.Sensors
{
    public class ColliderSensor : MonoBehaviour
    {
        private bool activeZone;

        public bool GetActiveZone() {  return activeZone; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                activeZone = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                activeZone = false;
            }
        }
    }
}
