using UnityEngine;

namespace Game.Sensors
{
    public abstract class GameSensor : MonoBehaviour
    {
        public bool sensorState;

        public bool GetState() {  return sensorState; }

    }
}
