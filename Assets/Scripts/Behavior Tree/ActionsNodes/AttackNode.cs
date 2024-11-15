namespace AI.BehaviorTree.Nodes
{

    public class AttackNode : ActionNode
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
            runner.Animator.Play("Punch01");
            return State.Success;
        }
        #endregion
    }
}
