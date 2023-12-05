using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] int jumpForce = 900;

    // private moving 
    private Rigidbody2D _rb;
    private float _inputX;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // move
        _rb.velocity = new Vector2(_inputX * speed, _rb.velocity.y);
    }
}
