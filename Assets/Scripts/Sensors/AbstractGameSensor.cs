using UnityEngine;

namespace Game.Sensors
{
    public abstract class GameSensor : MonoBehaviour
    {
        protected bool _sensorState;

        public bool GetState() {  return _sensorState; }

    }
}
