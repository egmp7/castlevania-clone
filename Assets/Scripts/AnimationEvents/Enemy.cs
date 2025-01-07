using UnityEngine;

namespace Enemy.Controller
{
    public class Enemy : MonoBehaviour
    {
        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void OnAnimationEnd()
        {
            animator.Play("Idle");
        }

        public void OnAttackAnimation()
        {
            Debug.Log("Enemy Attack"); 
        }
    }
}

