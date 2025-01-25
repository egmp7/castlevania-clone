using UnityEngine;
using HutongGames.PlayMaker;
using BehaviorDesigner.Runtime;

namespace egmp7.Playmaker.Actions
{
    [ActionCategory("Events")]
    [HutongGames.PlayMaker.Tooltip("Sends an event to Behavior Designer")]
    public class SendEventBT : FsmStateAction
    {
        public FsmOwnerDefault targetGameObject;
        //public FsmInt intValue;
        public FsmString eventName;

        public override void Reset()
        {
            targetGameObject = null;
            //intValue = null;
            eventName = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(targetGameObject);
            if (go == null)
            {
                Debug.LogError("Target GameObject is null. Cannot find BehaviorTree component.");
                Finish();
                return;
            }

            var behaviorTree = go.GetComponent<BehaviorTree>();
            if (behaviorTree == null)
            {
                Debug.LogError("BehaviorTree component not found on the target GameObject.");
                Finish();
                return;
            }

            behaviorTree.SendEvent(eventName.Value);
            Finish();
        }
    }
}
