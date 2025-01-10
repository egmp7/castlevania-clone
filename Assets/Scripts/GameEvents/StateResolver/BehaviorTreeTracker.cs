using BehaviorDesigner.Runtime;
using UnityEngine;

namespace egmp7.AI.EventHandlers
{
    public class BehaviorTreeEventHandler : MonoBehaviour
    {

        private BehaviorTree _behaviorTree;


        private void Awake()
        {
            if (TryGetComponent(out _behaviorTree))
            {
                //Debug.LogError("BehaviorTree not initialized");
            }
        }

        public  void TriggerHurtEvent()
        {
            _behaviorTree.SendEvent("HurtEvent");
        }
    }
}

