using UnityEngine;
using HutongGames.PlayMaker;

namespace egmp7.Playmaker.Actions
{
    [ActionCategory(ActionCategory.Debug)]
    [HutongGames.PlayMaker.Tooltip("Logs the value of a Text Field to the Unity Console.")]
    public class LogTextField : FsmStateAction
    {
        //[UIHint(UIHint.Variable)]
        [HutongGames.PlayMaker.Tooltip("The text field to log.")]
        public FsmString textField;

        [HutongGames.PlayMaker.Tooltip("Add a custom prefix to the log message.")]
        public FsmString prefix;

        [HutongGames.PlayMaker.Tooltip("Repeat every frame. Useful if the text field changes.")]
        public bool everyFrame;

        public override void Reset()
        {
            textField = null;
            prefix = "";
            everyFrame = true;
        }

        public override void OnEnter()
        {
            LogText();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            LogText();
        }

        private void LogText()
        {
            if (textField == null)
            {
                Debug.LogWarning("LogTextField: Text Field is not set.");
                return;
            }

            string logMessage = string.IsNullOrEmpty(prefix.Value)
                ? textField.Value
                : $"{prefix.Value}: {textField.Value}";

            Debug.Log(logMessage);
        }
    }
}

