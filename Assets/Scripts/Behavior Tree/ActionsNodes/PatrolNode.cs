using UnityEngine;

namespace AI.BehaviorTree.Nodes
{
    public class PatrolNode : ActionNode
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
            Debug.Log("Patrol Node");
            return State.Success;
        }
        #endregion
    }
}