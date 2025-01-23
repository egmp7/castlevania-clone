using UnityEngine;
using HutongGames.PlayMaker;
using InputCommands.Buttons;

namespace CustomPlayMakerActions
{
    [ActionCategory("Input")]
    [HutongGames.PlayMaker.Tooltip("Triggers the next action based on ButtonA state (pressed or released).")]
    public class ButtonAAction : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("Specify whether to trigger on press or release.")]
        public FsmBool triggerOnPress;

        [HutongGames.PlayMaker.Tooltip("The name of the InputAction for ButtonA.")]
        public FsmString actionName;

        private ButtonA buttonA;

        public override void Reset()
        {
            triggerOnPress = true;
            actionName = "ActionA";
        }

        public override void OnEnter()
        {
            try
            {
                buttonA = new ButtonA();

                // Override inputAction if actionName is specified
                if (!string.IsNullOrEmpty(actionName.Value))
                {
                    buttonA = new ButtonA
                    {
                        //inputAction = buttonA.FindInputAction(actionName.Value)
                    };
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (buttonA == null)
            {
                Finish();
                return;
            }

            bool conditionMet = triggerOnPress.Value ? buttonA.IsPressed() : buttonA.IsReleased();

            if (conditionMet)
            {
                Fsm.Event(FsmEvent.Finished);
                Finish();
            }
        }
    }
}
