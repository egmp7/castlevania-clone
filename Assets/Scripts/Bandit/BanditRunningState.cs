using UnityEngine;

public class BanditRunningState : StateMachineBehaviour
{
    [SerializeField] float speed = 2.5f;
    [SerializeField] float attackRange = 3.0f;

    private Transform player;
    private Rigidbody2D rb;
    private BanditController _banditController;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // instatiate
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        _banditController = animator.GetComponent<BanditController>(); 
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        // Moving Enemy 

        // enemy position
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        // new enemy position 
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        // move to new position
        rb.MovePosition(newPos);
        // enemy flip if needs to
        _banditController.LookAtPlayer();

        // Attacking

        // check if enemy is in player's range
        if (Vector2.Distance(animator.transform.position, player.position) <= attackRange )
        {
            // trigger animation
            animator.SetTrigger("Attack");

        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}
