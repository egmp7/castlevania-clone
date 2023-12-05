using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float HorizontalSpeed;
   
    private float _playerVelocityX;
    private PlayerController2D _controller;
    private bool _jump = false;
    private bool _crouch = false;

    private Animator _animator;
    private int _animIDIsMoving;
    private int _animIDIsJumping;

    private void Start()
    {
        _controller = GetComponent<PlayerController2D>();
        _animator = GetComponent<Animator>();

        AssignAnimationIDs();
    }

    private void Update()
    {
        _playerVelocityX = Input.GetAxisRaw("Horizontal") * HorizontalSpeed ;
        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetBool(_animIDIsJumping, true);
            _jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            _crouch = true;
        }

        if (Input.GetButtonUp("Crouch"))
        {
            _crouch = false;
        }

        // Animation
        if (Input.GetAxisRaw("Horizontal") == 0.0f) _animator.SetBool(_animIDIsMoving, false);
        else _animator.SetBool(_animIDIsMoving, true);
    }

    private void FixedUpdate()
    {
        _controller.Move(_playerVelocityX, _crouch, _jump);
        _jump = false;
    }

    private void AssignAnimationIDs()
    {
        _animIDIsMoving = Animator.StringToHash("isMoving");
        _animIDIsJumping = Animator.StringToHash("isJumping");
    }

    public void OnPlayerLand()
    {
        _animator.SetBool(_animIDIsJumping, false);
    }
}
