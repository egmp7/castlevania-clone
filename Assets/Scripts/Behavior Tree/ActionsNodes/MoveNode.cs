namespace AI.BehaviorTree.Nodes
{

    public class MoveNode : ActionNode
    {
        #region Overrides of Node
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            runner.Animator.Play("Run");
            return State.Success;
        }
        #endregion
    }
}
