using UnityEngine;

namespace AI.BehaviorTree
{

    public class WaitNode : ActionNode
    {
        public WaitNode(BehaviorTreeRunner input): base(input) { }

        public float duration = 1f;

        private float m_startTime;

        #region Overrides of Node

        /// <inheritdoc />
        protected override void OnStart() =>
            m_startTime = Time.time;

        /// <inheritdoc />
        protected override void OnStop() { }

        /// <inheritdoc />
        protected override State OnUpdate()
        {
            return Time.time - m_startTime > duration ?
                    State.Success : State.Running;
        }

        #endregion
    }
}