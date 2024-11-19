using UnityEngine;
using UnityEngine.InputSystem;

namespace InputCommands.Buttons
{

    public abstract class Button
    {
        protected InputAction inputAction;
        private bool state;

        public bool IsPressed()
        {
            #region Button Algorithm
            if (inputAction.IsPressed() && !state)
            {
                state = true;
                return true;
            }
            else if (!inputAction.IsPressed())
            {
                state = false;
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
