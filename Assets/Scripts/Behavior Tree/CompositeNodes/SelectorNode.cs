using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public class SelectorNode : CompositeNode
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
            if (children == null || children.Count < 1)
            {
                Debug.LogWarning("Selector Node has no children.");
                return State.Failure;
            }

            while (m_current < children.Count)
            {
                var childState = children[m_current]!.Update();

                switch (childState)
                {
                    case State.Running:
                        return State.Running;
                    case State.Success:
                        return State.Success;
                    case State.Failure:
                        m_current++;
                        break;
                    default:
                        return State.Failure;
                }
            }

            return State.Failure;
        }

        #endregion
    }
}
