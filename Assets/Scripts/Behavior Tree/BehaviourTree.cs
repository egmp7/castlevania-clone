using UnityEngine;
using AI.BehaviorTree.Nodes;

namespace AI.BehaviorTree
{
    public class BehaviorTree
    {
        public Node rootNode;
        public Node.State treeState = Node.State.Running;
        private bool m_hasRootNode;

        public Node.State Update()
        {
            if (!m_hasRootNode)
            {
                m_hasRootNode = rootNode != null;

                if (!m_hasRootNode)
                {
                    Debug.LogWarning("BehaviorTree needs a root node in order to properly run. Please add one.");
                }
            }

            if (m_hasRootNode)
            {
                if (treeState == Node.State.Running)
                    treeState = rootNode.Update();
            }
            else
            {
                treeState = Node.State.Failure;
            }

            return treeState;
        }
    }
}
