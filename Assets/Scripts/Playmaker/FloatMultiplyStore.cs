using HutongGames.PlayMaker;

namespace egmp7.Playmaker.Actions
{

    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Multiplies one Float by another and optionally stores the result in a new variable.")]
    public class FloatMultiplyStore : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The float variable to multiply.")]
        public FsmFloat value1;

        [RequiredField]
        [Tooltip("Multiply the float variable by this value.")]
        public FsmFloat value2;

        [UIHint(UIHint.Variable)]
        [Tooltip("Optionally store the result in a new variable.")]
        public FsmFloat storeResult;

        [Tooltip("Repeat every frame. Useful if the variables are changing.")]
        public bool everyFrame;

        public override void Reset()
        {
            value1 = null;
            value2 = null;
            storeResult = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoMultiply();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoMultiply();
        }

        private void DoMultiply()
        {
            float result = value1.Value * value2.Value;

            if (!storeResult.IsNone)
            {
                storeResult.Value = result;
            }
            else
            {
                value1.Value = result;
            }
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            return ActionHelpers.AutoName(this, value1, value2);
        }
#endif
    }
}
