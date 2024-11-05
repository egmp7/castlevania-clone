using UnityEngine;

[CreateAssetMenu(fileName = "BehaviorTree", menuName = "Behavior Tree")]
public class BehaviorTree : ScriptableObject 
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
                Debug.LogWarning($"{name} needs a root node in order to properly run. Please add one.", this);
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
