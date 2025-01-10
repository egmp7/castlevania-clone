
using UnityEngine;

namespace Game.Trackers
{
    public abstract class StateTracker : MonoBehaviour
    {
        public abstract bool IsBlockState();

        public abstract void TriggerHurtEvent();
    }
}