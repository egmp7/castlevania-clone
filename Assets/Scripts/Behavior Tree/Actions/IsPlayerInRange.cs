using UnityEngine;

namespace AI.BehaviorTree
{

    public class IsPlayerInRange : ActionNode
    {

        public IsPlayerInRange(BehaviorTreeRunner input) : base(input) { }

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
            Debug.Log("Player in Range");
        }

        protected override State OnUpdate()
        {
            if (input.AttackZoneSensor.ActiveZone)
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
