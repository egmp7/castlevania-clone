using UnityEngine;
using UnityEngine.InputSystem;

namespace InputCommands.Buttons
{

    public abstract class Button
    {
        protected InputAction inputAction;
        private bool _stateIn = false;
        private bool _stateOut = true;

        public bool IsPressed()
        {
            #region Button Algorithm
            if (inputAction.IsPressed() && !_stateIn)
            {
                _stateIn = true;
                return true;
            }
            else if (!inputAction.IsPressed())
            {
                _stateIn = false;
            }
            return false;
            #endregion
        }

        public bool IsReleased()
        {

            #region Button Algorithm
            if (!inputAction.IsPressed() && _stateOut)
            {
                _stateOut = false;
                return true;
            }
            else if (inputAction.IsPressed())
            {
                _stateOut = true;
            }
            return false;
            #endregion
        }

        protected InputAction FindInputAction(string actionName)
        {
            var action = InputSystem.actions.FindAction(actionName);
            if (action == null)
            {
                Debug.LogError($"Action '{actionName}' not found");
                throw new System.Exception($"InputAction '{actionName}' is required but was not found.");
            }
            return action;
        }
    }
}
