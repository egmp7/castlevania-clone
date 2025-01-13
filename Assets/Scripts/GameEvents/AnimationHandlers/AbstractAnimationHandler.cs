using UnityEngine;

namespace Game.AnimationEvent
{
    [RequireComponent(typeof(Animator))]

    public abstract class AnimationHandler : MonoBehaviour
    {
        public abstract void HandleAnimationEnd();

        public abstract void HandleAnimationAttack();
    }
}
