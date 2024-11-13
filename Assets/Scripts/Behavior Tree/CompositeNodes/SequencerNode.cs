using UnityEngine;

namespace AI.BehaviorTree.Nodes
{

    public class SequencerNode : CompositeNode
    {
        private int m_current;

        #region Overrides of Node

        /// <inheritdoc />
        protected override void OnStart()
        {
            m_current = 0;
        }

        /// <inheritdoc />
        protected override void OnStop() { }

        /// <inheritdoc />
        protected override State OnUpdate()
        {
            if (children == null && children.Count < 1)
            {
                Debug.LogWarning("Sequencer Node has no children.");
                return State.Failure;
            }

            switch (children[m_current]!.Update())
            {
                case State.Running:
                    return State.Running;
                case State.Success:
                    m_current++;
                    break;
                case State.Failure:
                    return State.Failure;
                default:
                    return State.Failure;
            }

            return m_current == children.Count ? State.Success : State.Running;
        }

        #endregion
    }
}