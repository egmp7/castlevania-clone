using UnityEngine;

namespace Game.Sensors
{
    public abstract class ColliderSensor : MonoBehaviour
    {
        protected bool sensorState;

        public bool GetState() {  return sensorState; }

    }
}
