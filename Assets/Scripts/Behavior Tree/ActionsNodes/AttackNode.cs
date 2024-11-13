using UnityEngine;

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
            Debug.Log("Attack Node");
            return State.Success;
        }
        #endregion
    }
}
