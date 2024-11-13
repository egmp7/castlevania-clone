namespace AI.BehaviorTree.Nodes
{
    public class RepeatNode : DecoratorNode
    {
        #region Overrides of Node

        /// <inheritdoc />
        protected override void OnStart() { }

        /// <inheritdoc />
        protected override void OnStop() { }

        /// <inheritdoc />
        protected override State OnUpdate()
        {
            child.Update();
            return State.Running;
        }

        #endregion
    }
}