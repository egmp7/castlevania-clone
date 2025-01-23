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
        public FsmFloat floatVariable;

        [RequiredField]
        [Tooltip("Multiply the float variable by this value.")]
        public FsmFloat multiplyBy;

        [UIHint(UIHint.Variable)]
        [Tooltip("Optionally store the result in a new variable.")]
        public FsmFloat storeResult;

        [Tooltip("Repeat every frame. Useful if the variables are changing.")]
        public bool everyFrame;

        public override void Reset()
        {
            floatVariable = null;
            multiplyBy = null;
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
            float result = floatVariable.Value * multiplyBy.Value;

            if (!storeResult.IsNone)
            {
                storeResult.Value = result;
            }
            else
            {
                floatVariable.Value = result;
            }
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            return ActionHelpers.AutoName(this, floatVariable, multiplyBy);
        }
#endif
    }
}
