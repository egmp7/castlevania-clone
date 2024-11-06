namespace AI.BehaviorTree
{

    public abstract class ActionNode : Node
    {
        protected BehaviorTreeRunner input;

        protected ActionNode(BehaviorTreeRunner input)
        {
            this.input = input;
        }
    }
}