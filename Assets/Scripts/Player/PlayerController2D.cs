using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;

    [Header ("Jump")]
    [SerializeField] int jumpForce = 900;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] [Range(0f, 2f)] float boxCastDistance = 1f;

    [Header("Boundary")]
    [SerializeField] float yBoundary = -30f;

    [SerializeField][Range(0.02f, 2f)] float movementReloadTime;
    [SerializeField] float enemyCollisionForce;

    // animation
    private Animator _animator;
    private int _idleAnimState = 0;
    private int _runningAnimState = 1;
    private bool _facingRight = true;

    // force 
    private Rigidbody2D _rb;
    private float _inputX;

    // sensors
    private bool _grounded;
    private Collider2D _collider;

    // colliding with enemy
    private PlayerHealth _playerHealth;
    private bool _canMove = true;
    private float movementTime;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // check if player is grounded
        _grounded = GroundCheck(_collider.bounds,boxCastDistance);
        // grounded animation
        _animator.SetBool("Grounded", _grounded);
        // input from player
        _inputX = Input.GetAxisRaw("Horizontal");
        // jump
        if (Input.GetKeyDown("space") && _grounded) Jump();
        // running animation
        if (_canMove) AnimateRunning(_inputX);
        //Set AirSpeed in animator
        _animator.SetFloat("AirSpeedY", _rb.velocity.y); 
        // Update _canMove
        if (Time.time > movementTime) _canMove = true;
    }

    private void FixedUpdate()
    {
        // move
        if (_canMove) _rb.velocity = new Vector2(_inputX * speed, _rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // collision with enemy
        if ((enemyLayer.value & 1 << collision.gameObject.layer) != 0)
        {
            // take damage
            _playerHealth.TakeDamage(30);
            // stop moving
            _canMove = false;
            // update for next moving 
            movementTime = Time.time + movementReloadTime;
            // enemy Animation State set to run
            collision.gameObject.GetComponent<Animator>().SetInteger("AnimState", 2);
            // get direction between player and enemy
            Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
            //reset velocity
            _rb.velocity = Vector3.zero;
            // impuse a force sp the player moves when touched
            if (direction.y > 0.1 ) _rb.AddForce(direction * enemyCollisionForce * 1.01f, ForceMode2D.Impulse);
            if (direction.y > 0.3) _rb.AddForce(direction * enemyCollisionForce * 1.02f, ForceMode2D.Impulse);
            if (direction.y > 0.5) _rb.AddForce(direction * enemyCollisionForce * 1.03f, ForceMode2D.Impulse);
            if (direction.y > 0.7) _rb.AddForce(direction * enemyCollisionForce * 1.04f, ForceMode2D.Impulse);
            if (direction.y > 0.9) _rb.AddForce(direction * enemyCollisionForce * 1.06f, ForceMode2D.Impulse);
            else _rb.AddForce(direction * enemyCollisionForce,ForceMode2D.Impulse);
        }

    }

    public bool isOutOfBoundary()
    {
        return transform.position.y < yBoundary;
    }

    private bool GroundCheck(Bounds bounds,float distance)
    {
        // BoxCast parameters
        Vector3 origin = bounds.center;
        Vector3 size = bounds.size;
        float angle = 0f;
        Vector2 direction = Vector2.down;
        Color rayColor;

        // Get ground collisions 
        RaycastHit2D hit =  Physics2D.BoxCast(origin,size,angle,direction,distance,whatIsGround);

        // Debug BoxCast
        if (hit.collider != null) rayColor = Color.yellow; 
        else rayColor = Color.red;
        Utilities.DebugDrawBoxCast(origin, size, angle, direction,distance, rayColor);
       
        // Check if collide with ground
        if (hit.collider != null) return true;
        else return false;
    }

    private void Jump()
    {
        //_grounded = false;
        _animator.SetTrigger("Jump");
        //_animator.SetBool("Grounded", _grounded);
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        //_groundSensor.Disable(0.2f);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void AnimateRunning (float input)
    {
        if (input > 0 && !_facingRight) Flip();
        if (input < 0 && _facingRight) Flip();
        if (input == 0) _animator.SetInteger("AnimState", _idleAnimState);
        if (input != 0) _animator.SetInteger("AnimState", _runningAnimState);
    }
}
