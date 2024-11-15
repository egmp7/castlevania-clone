

using UnityEngine;

namespace AI.BehaviorTree.Runners
{

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]

    public abstract class Runner : MonoBehaviour
    {
        [SerializeField] Transform[] PatrolPoints;

        [HideInInspector] public Rigidbody2D Rigidbody2D;
        [HideInInspector] public Animator Animator;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }
    }
}
