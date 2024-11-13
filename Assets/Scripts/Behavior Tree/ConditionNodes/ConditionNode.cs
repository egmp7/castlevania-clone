using System;

namespace AI.BehaviorTree.Nodes
{
    public class ConditionNode : Node
    {
        // Delegate to hold the condition function
        private readonly Func<bool> _condition;

        // Constructor to pass in a condition
        public ConditionNode(Func<bool> condition)
        {
            _condition = condition;
        }

        protected override void OnStart() { }
        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            // Check the condition
            return _condition() ? State.Success : State.Failure;
        }
    }
}
