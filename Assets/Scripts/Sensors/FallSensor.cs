using UnityEngine;

namespace Game.Sensors
{

    public class FallSensor : GameSensor
    {

        private Rigidbody2D playerRigidBody;

        // Start is called before the first frame update
        void Start()
        {
            _sensorTag = "Player";
            GameObject player = GameObject.FindWithTag(_sensorTag);

            if (player == null)
            {
                Debug.LogError("Player not found! Make sure your Player GameObject is tagged as 'Player'.");
            }

            playerRigidBody = player.GetComponent<Rigidbody2D>();

            if (playerRigidBody == null)
            {
                Debug.LogError("Rigid Body not found! Make sure your Player has a RigidBody.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (playerRigidBody != null)
            {
                if (playerRigidBody.velocity.y < 0)
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
