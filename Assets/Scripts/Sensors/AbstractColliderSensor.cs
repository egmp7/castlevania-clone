using UnityEngine;

namespace Game.Sensors
{
    public abstract class GameSensor : MonoBehaviour
    {
        protected bool sensorState;

        public bool GetState() {  return sensorState; }

    }
}
