using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] int jumpForce = 900;

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
    private PlayerSensor _groundSensor;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _groundSensor = transform.Find("GroundSensor").GetComponent<PlayerSensor>();
    }

    private void Update()
    {
        // check if player is grounded
        UpdateGrounded();
        // input from player
        _inputX = Input.GetAxisRaw("Horizontal");
        // jump
        if (Input.GetKeyDown("space") && _grounded) Jump();
        // running animation
        AnimateRunning(_inputX);
        //Set AirSpeed in animator
        _animator.SetFloat("AirSpeedY", _rb.velocity.y);
    }

    private void FixedUpdate()
    {
        // move
        _rb.velocity = new Vector2(_inputX * speed, _rb.velocity.y);
    }

    private void UpdateGrounded()
    {
        if (!_grounded && _groundSensor.State())
        {
            _grounded = true;
            _animator.SetBool("Grounded", _grounded);
        }
    }

    private void Jump()
    {
        _grounded = false;
        _animator.SetTrigger("Jump");
        _animator.SetBool("Grounded", _grounded);
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        _groundSensor.Disable(0.2f);
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
