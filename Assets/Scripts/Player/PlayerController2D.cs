using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;

    [Header("Movement")]
    [Tooltip("Moving speed")]
    [SerializeField] float speed = 2.0f;

    [Tooltip("The strength when the player collides with an enemy")]
    [SerializeField] float collisionForceMagnitude;
    
    [Tooltip("How long a player movement is disabled when receives damage")]
    [SerializeField][Range(0.02f, 2f)] float movementDisableDuration;

    [Tooltip("Boundary of the game, if passes this treshold is consider game over")]
    [SerializeField] float yBoundary = -30f;

    [Header ("Jump")]
    [Tooltip("Jumping Strength")]
    [SerializeField] int jumpForce = 900;

    [Tooltip("Distance that expands the jumping boxcast")]
    [SerializeField] [Range(0f, 2f)] float boxCastDistance = 1f;

    [Header("Attack")]
    [Tooltip("Game Object for the attack reference point")]
    [SerializeField] Transform attackPoint;

    [Tooltip("Affects the attacking Speed")]
    [SerializeField] [Range(0f, 1f)] float attackSpeedRatio = 0.2f;

    [Tooltip("Radius of the attack")]
    [SerializeField] [Range(0.1f,2f)] float attackRadius = 0.5f;

    [Tooltip("Waiting rate for next attack")]
    [SerializeField] [Range(0.1f,4f)] float attackRate = 2.0f;

    [Tooltip("Integer, how much damage is the attack")]
    [SerializeField] int enemyDamage = 30;

    [Header("Health")]
    [Tooltip("Health Bar game object")]
    [SerializeField] HealthBarController healthBar;
    [Tooltip("Max health")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] float invulnerabilityDuration = 1f;


    private Animator _animator;
    private Rigidbody2D _rb;
    private Collider2D _collider;

    // input
    private float _inputX;

    // general
    private bool _facingRight = true;
    private bool _grounded;

    // movement related
    private bool _isMovementEnabled  = true;
    private float _disableMovementUntil ;
    private bool _isAttacking = false;
    private float _nextAttackCooldownEndTime = 0.0f;

    // Health
    private int _currentHealth;
    private bool _canTakeDamage = true;
    private float _nextDamageEnabledTime;

    private void OnEnable()
    {
        EventManager.onPlayerDeath += Die;
    }
    private void OnDisable()
    {
        EventManager.onPlayerDeath -= Die;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();

        _currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        // Input from player
        _inputX = Input.GetAxisRaw("Horizontal");

        // Check if player is grounded
        _grounded = GroundCheck(_collider.bounds,boxCastDistance);
        // Check if movement is allowed
        if (Time.time > _disableMovementUntil) _isMovementEnabled = true;
        // Check if can take damage
        if (Time.time > _nextDamageEnabledTime) _canTakeDamage = true;

        // Grounded animation
        _animator.SetBool("Grounded", _grounded);
        // Running animation
        if (_isMovementEnabled ) AnimateRunning(_inputX);
        // Set AirSpeed in animator
        _animator.SetFloat("AirSpeedY", _rb.velocity.y); 

        // Jump
        if (Input.GetKeyDown("space") && _grounded) Jump();
        // Attack
        if (IsAttackCooldownOver() && Input.GetButtonDown("Fire1")) Attack();
    }

    private void FixedUpdate()
    {
        // move
        if (_isMovementEnabled  && !_isAttacking) _rb.velocity = new Vector2(_inputX * speed, _rb.velocity.y);
        if (_isAttacking) _rb.velocity = new Vector3(_inputX * speed * attackSpeedRatio, _rb.velocity.y,0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // collision with enemy
        if ((enemyLayer.value & 1 << collision.gameObject.layer) != 0)
        {
            // take damage
            TakeDamage(10);
            // stop moving
            _isMovementEnabled  = false;
            // update for next moving 
            _disableMovementUntil  = Time.time + movementDisableDuration;
            // enemy Animation State set to run
            collision.gameObject.GetComponent<Animator>().SetInteger("AnimState", 2);
            // get direction between player and enemy
            Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
            //reset velocity
            _rb.velocity = Vector3.zero;
            // impuse a force sp the player moves when touched
            if (direction.y > 0.1 ) _rb.AddForce(direction * collisionForceMagnitude * 1.01f, ForceMode2D.Impulse);
            if (direction.y > 0.3) _rb.AddForce(direction * collisionForceMagnitude * 1.02f, ForceMode2D.Impulse);
            if (direction.y > 0.5) _rb.AddForce(direction * collisionForceMagnitude * 1.03f, ForceMode2D.Impulse);
            if (direction.y > 0.7) _rb.AddForce(direction * collisionForceMagnitude * 1.04f, ForceMode2D.Impulse);
            if (direction.y > 0.9) _rb.AddForce(direction * collisionForceMagnitude * 1.06f, ForceMode2D.Impulse);
            else _rb.AddForce(direction * collisionForceMagnitude,ForceMode2D.Impulse);
        }
    }

    public bool isOutOfBoundary()
    {
        return transform.position.y < yBoundary;
    }

    public void AttackEnemiesInRange()
    {
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        // Damaged them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BanditController>().TakeDamage(enemyDamage);
        }
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack1");
        _nextAttackCooldownEndTime = Time.time + 1 / attackRate;
    }

    private bool IsAttackCooldownOver()
    {
        if (Time.time > _nextAttackCooldownEndTime) return true;
        return false;
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
        RaycastHit2D hit =  Physics2D.BoxCast(origin,size,angle,direction,distance,groundLayer);

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
        if (input == 0) _animator.SetInteger("AnimState", 0);
        if (input != 0) _animator.SetInteger("AnimState", 1);
    }

    public void SetIsAttacking(bool boolean)
    {
        _isAttacking = boolean;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void TakeDamage(int damage)
    {
        if (!_canTakeDamage) return;

        // Trigger hurt animation
        _animator.SetTrigger("Hurt");
        // Reduce health
        _currentHealth -= damage;
        healthBar.SetHealth(_currentHealth);
        _canTakeDamage = false;
        _nextDamageEnabledTime = Time.time + invulnerabilityDuration;
    }

    private void Die()
    {
        // Trigger death animation
        _animator.SetTrigger("Death");
        // Disable collider
        GetComponent<PlayerController2D>().enabled = false;
        // Stop moving
        _rb.velocity = Vector3.zero;
        // Disable this script
        this.enabled = false;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
}
