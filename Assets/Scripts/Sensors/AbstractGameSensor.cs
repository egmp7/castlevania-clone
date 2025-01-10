using UnityEngine;

namespace Game.Sensors
{
    public abstract class GameSensor : MonoBehaviour
    {
        protected string _sensorTag;
        protected bool _sensorState;

        private void Start()
        {

            GameObject foundObject = GameObject.FindGameObjectWithTag(_sensorTag);
            if (foundObject == null)
            {
                ErrorManager.LogMissingGameObjectWithTag(_sensorTag);
            }
            
        }

        public bool GetState() {  return _sensorState; }

    }
}
