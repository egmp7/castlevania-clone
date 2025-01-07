using Game.AnimationEvent.Receiver;
using Game.AnimationEvent.Source;
using UnityEngine;

namespace Game.AnimationEvent
{
    [RequireComponent(typeof(DamageProcessor))]
    [RequireComponent(typeof(DamageListener))]

    public abstract class AnimationHandler : MonoBehaviour
    {
        public abstract void HandleAnimationEnd();

        public abstract void HandleAnimationAttack();
    }
}
