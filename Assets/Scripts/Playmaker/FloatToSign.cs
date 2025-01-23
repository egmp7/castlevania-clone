using HutongGames.PlayMaker;

namespace egmp7.Playmaker.Actions
{
    [ActionCategory("Math")]
    [Tooltip("Takes a float value and outputs 1, -1, or 0 based on its value.")]
    public class FloatToSign : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The float value to evaluate.")]
        public FsmFloat inputFloat;

        [UIHint(UIHint.Variable)]
        [Tooltip("The output integer: 1, -1, or 0.")]
        public FsmInt outputSign;

        [Tooltip("Repeat every frame. Useful if the input value changes over time.")]
        public bool everyFrame;

        public override void Reset()
        {
            inputFloat = null;
            outputSign = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            EvaluateSign();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            EvaluateSign();
        }

        private void EvaluateSign()
        {
            if (inputFloat.Value > 0)
            {
                outputSign.Value = 1;
            }
            else if (inputFloat.Value < 0)
            {
                outputSign.Value = -1;
            }
            else
            {
                outputSign.Value = 0;
            }
        }
    }
}
