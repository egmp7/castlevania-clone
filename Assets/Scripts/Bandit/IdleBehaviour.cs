using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    [SerializeField] float speed;
    [SerializeField][Range(0.2f, 2f)] float LookPointRange;
    [SerializeField][Range(0.2f, 3f)] float PatrolRange;

    private BanditController _banditController;
    private Transform _idleCastPoint;
    private Transform _idleTargetPoint;
    private Vector3 _initPos;
    private Vector3 _targetPos;
    private bool _towardsTarget = true;

    private Rigidbody2D _rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _idleCastPoint = animator.transform.Find("IdleCastPoint");
        _idleTargetPoint = animator.transform.Find("IdleTargetPoint");

        _rb = animator.GetComponent<Rigidbody2D>();
        _banditController = animator.GetComponent<BanditController>();

        _initPos = _rb.position;
        _targetPos = new Vector3(_idleTargetPoint.position.x,_rb.position.y);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Patrol();
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

    private void Patrol()
    {
        Vector2 newPos;

        // check if has reached target position
        if (Vector3.Distance(_rb.position, _targetPos) < 0.5f) _towardsTarget = false;
        // check if has reached initial position
        if (Vector3.Distance(_rb.position, _initPos) < 0.5f) _towardsTarget = true;

        // new position towards target
        if (_towardsTarget) newPos = Vector2.MoveTowards(_rb.position, _targetPos, speed * Time.fixedDeltaTime);
        // new position towards initial position
        else newPos = Vector2.MoveTowards(_rb.position, _initPos, speed * Time.fixedDeltaTime);

        // move 
        _rb.MovePosition(newPos);

        // flip
        if (_towardsTarget && !_banditController.isFlipped)
        {
            _banditController.Flip();
            _banditController.SetIsFlipped(true);
        }
        if (!_towardsTarget && _banditController.isFlipped)
        { 
            _banditController.Flip();
            _banditController.SetIsFlipped(false);
        }
    }
}
