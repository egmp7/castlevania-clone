using UnityEngine;

public class BanditController : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] bool isFlipped = false;

    [SerializeField] Transform attackPoint;
    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRange = 1.0f;
    [SerializeField] LayerMask attackMask;
    [SerializeField] int attackDamage = 20;

    Animator _animator;
    private int _currentHealth;
    private Transform _player;
    private Rigidbody2D _rb;

    private void Start()
    {
        _currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        _animator.SetInteger("AnimState", 0);
        _player = GameObject.FindWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EventManager.onPlayerDeath += StopAttacking;
    }
    private void OnDisable()
    {
        EventManager.onPlayerDeath -= StopAttacking;
    }

    public void Attack()
    {
        // get player collision
        Collider2D colInfo = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackMask);

        // if player collision
        if (colInfo != null)
        {
            // produce player damage
            colInfo.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage) 
    {
        _currentHealth -= damage;
        _animator.SetTrigger("Hurt");
        _animator.SetInteger("AnimState", 2);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void LookAtPlayer() 
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1;

        if (transform.position.x < _player.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0.0f, 180.0f, 0.0f);
            isFlipped = false;
        }

            if (transform.position.x > _player.transform.position.x && !isFlipped )
        {
            transform.localScale = flipped;
            transform.Rotate(0.0f, 180.0f, 0.0f);
            isFlipped = true;
        }
    }

    private void Die()
    {
        // Die animation
        _animator.SetBool("Death", true);

        // Disable enemy
        Destroy(_rb);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    private void StopAttacking()
    {
        _animator.SetInteger("AnimState", 1);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
