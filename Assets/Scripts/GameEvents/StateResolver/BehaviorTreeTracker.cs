using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Game.Trackers
{
    public class BehaviorTreeTracker : StateTracker
    {
        private BehaviorTree _behaviorTree;
        private readonly string _blockTaskName = "Block";

        private void Awake()
        {
            if (TryGetComponent(out _behaviorTree))
            {
                Debug.LogError("BehaviorTree not initialized");
            }
        }

        public override bool IsBlockState()
        {
            return _behaviorTree.FindTaskWithName(_blockTaskName) != null;
        }
    }
}

