using System.Collections.Generic;

namespace AI.BehaviorTree
{

    public abstract class CompositeNode : Node
    {
        public List<Node> children = new List<Node>();
    }
}