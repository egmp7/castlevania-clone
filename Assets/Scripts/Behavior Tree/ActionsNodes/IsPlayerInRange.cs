using UnityEngine;

namespace AI.BehaviorTree.Nodes
{

    public class IsPlayerInRange : ActionNode
    {

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
            Debug.Log("Player in Range");
        }

        protected override State OnUpdate()
        {
            if (true)
            {
                return State.Success;

            }
            else
            {
                return State.Running;
            }
        }
    }
}
