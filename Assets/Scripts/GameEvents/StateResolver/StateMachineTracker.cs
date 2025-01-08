using Player.StateManagement;
using UnityEngine;

namespace Game.Trackers
{
    public class StateMachineTracker : StateTracker
    {
        private StateMachine _stateMachine;

        private void Awake()
        {
            if (TryGetComponent(out _stateMachine))
            {
                Debug.LogError("StateMachine not initialized");
            }
        }

        public override bool IsBlockState()
        {
            return _stateMachine.GetCurrentState() is BlockState;
        }
    }
}

