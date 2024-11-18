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
            var reusableCommand = _inputListener.GetReusableCommands();

            if (reusableCommand != null)
            {
                _invoker.Execute(reusableCommand, _stateMachine);
            }

            var coupledCommand = _inputListener.GetCoupledCommands();

            if (coupledCommand != null)
            {
                _invoker.Execute(coupledCommand, _stateMachine);
            }
        }
    }
}