using Player.StateManagement;
using UnityEngine;
namespace InputCommands
{

    [RequireComponent(typeof(InputListener))]
    [RequireComponent(typeof(Invoker))]
    [RequireComponent(typeof(StateController))]
    public class Client : MonoBehaviour
    {
        private InputListener _inputListener;
        private Invoker _invoker;
        private StateController _stateController;

        void Awake()
        {
            _inputListener = GetComponent<InputListener>();
            _invoker = GetComponent<Invoker>();
            _stateController = GetComponent<StateController>();
        }

        void Update()
        {
            var reusableCommand = _inputListener.GetReusableCommands();

            if (reusableCommand != null)
            {
                _invoker.Execute(reusableCommand, _stateController);
            }

            var coupledCommand = _inputListener.GetCoupledCommands();

            if (coupledCommand != null)
            {
                _invoker.Execute(coupledCommand, _stateController);
            }
        }
    }
}