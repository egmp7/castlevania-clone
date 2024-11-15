using System.Collections.Generic;

namespace AI.BehaviorTree.Nodes
{

    public abstract class CompositeNode : Node
    {
        public List<Node> children = new List<Node>();
    }
}