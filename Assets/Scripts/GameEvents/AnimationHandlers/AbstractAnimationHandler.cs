using Game.AnimationEvent.Source;
using UnityEngine;

namespace Game.AnimationEvent
{
    [RequireComponent(typeof(DamageProcessor))]

    public abstract class AnimationHandler : MonoBehaviour
    {
        public abstract void HandleAnimationEnd();

        public abstract void HandleAnimationAttack();
    }
}
