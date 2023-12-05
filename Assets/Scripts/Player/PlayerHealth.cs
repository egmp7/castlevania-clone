using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    private Animator _animator;
    private Rigidbody2D _rb;

    private int _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        _animator.SetTrigger("Hurt");

        _currentHealth -= damage;

        if (_currentHealth <= 0) Die();

    }

    private void Die()
    {
        // trigger animation
        _animator.SetTrigger("Death");
        // disable colider
        GetComponent<Collider2D>().enabled = false;
        // disable rigid body
        _rb.isKinematic = true;
        // disable this script
        this.enabled = false;

    }
}
