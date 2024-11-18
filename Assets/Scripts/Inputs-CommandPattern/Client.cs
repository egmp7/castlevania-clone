using Player.StateManagement;
using UnityEngine;
namespace InputCommands
{

    [RequireComponent(typeof(InputListener))]
    [RequireComponent(typeof(Invoker))]
    [RequireComponent(typeof(StateMachine))]
    public class Client : MonoBehaviour

    {
        private InputListener _inputListener;
        private Invoker _invoker;
        private StateMachine _stateMachine;

        void Awake()
        {
            _inputListener = GetComponent<InputListener>();
            _invoker = GetComponent<Invoker>();
            _stateMachine = GetComponent<StateMachine>();
        }

        void Update()
        {
            var moveCommand = _inputListener.GetMoveCommands();

            if (moveCommand != null)
            {
                _invoker.Execute(moveCommand, _stateMachine);
            }

            var buttonCommand = _inputListener.GetButtonCommands();

            if (buttonCommand != null)
            {
                _invoker.Execute(buttonCommand, _stateMachine);
            }

            var sensorCommand = _inputListener.GetSensorCommands();
            if (sensorCommand != null)
            {
                _invoker.Execute(sensorCommand, _stateMachine);
            }
        }
    }
}