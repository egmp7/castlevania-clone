using UnityEngine;

namespace Game.Sensors
{

    public class FallSensor : GameSensor
    {

        private Rigidbody2D _playerRigidBody;
        private readonly string _sensorTag = "Player";

        // Start is called before the first frame update
        void Start()
        {
            GameObject player = GameObject.FindWithTag(_sensorTag);

            if (player == null)
            {
                ErrorManager.LogMissingGameObjectWithTag(_sensorTag);
            }

            
            if (!player.TryGetComponent(out _playerRigidBody))
            {
                ErrorManager.LogMissingComponent<Rigidbody2D>(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerRigidBody != null)
            {
                if (_playerRigidBody.velocity.y < 0)
                {
                    _sensorState = true;
                }
                else
                {
                    _sensorState = false;
                }
            }
        }
    }
}
