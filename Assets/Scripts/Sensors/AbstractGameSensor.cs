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
                Debug.LogWarning($"No GameObject found with tag: {_sensorTag}");
            }
            
        }

        public bool GetState() {  return _sensorState; }

    }
}
