using UnityEngine;

public class RunningBehavior : StateMachineBehaviour
{
    [SerializeField] float speed = 2.5f;
    [SerializeField] float attackRange = 3.0f;

    private Transform _player;
    private Rigidbody2D _rb;
    private BanditController _banditController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // instatiate
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = animator.GetComponent<Rigidbody2D>();
        _banditController = animator.GetComponent<BanditController>(); 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        // Target direction
        Vector3 target = (_player.position - animator.transform.position).normalized;
        // new velocity
        Vector3 velocity = new Vector3(target.x, _rb.velocity.normalized.y, target.z) * speed * Time.fixedDeltaTime;
        // update velocity
        _rb.velocity = velocity;

        // Check if the enemy is in the player's attack range
        if (Vector2.Distance(animator.transform.position, _player.position) <= attackRange)
        {
            // Trigger attack animation
            animator.SetTrigger("Attack");
        }
        // Flip the enemy if needed
        _banditController.LookAtPlayer();

   
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}
