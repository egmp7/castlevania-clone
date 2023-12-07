using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    [SerializeField][Range(0.2f, 2f)] float LookPointRange;
    [SerializeField][Range(0.2f, 3f)] float PatrolRange;

    private Transform _idleCastPoint;
    private string _castNamePoint = "IdleCastPoint";
    private Rigidbody2D _rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _idleCastPoint = animator.transform.Find(_castNamePoint);
        _rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckCast(animator);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

        private void CheckCast(Animator animator)
    {
        // get cast positions
        Vector3 castInitPos = _idleCastPoint.position;
        Vector3 castEndPos = _idleCastPoint.position + _idleCastPoint.right * LookPointRange;

        // get line cast
        RaycastHit2D hit = Physics2D.Linecast(
            _idleCastPoint.position,
            _idleCastPoint.position + _idleCastPoint.right * LookPointRange,
            1 << LayerMask.NameToLayer("Player"));

        // change animation if hit
        if (hit.collider != null)
            animator.SetInteger("AnimState", 2);

        // Debug
        if (hit.collider != null)
            Debug.DrawLine(castInitPos, castEndPos, Color.red);
        else
            Debug.DrawLine(castInitPos, castEndPos, Color.green);
    }
}
