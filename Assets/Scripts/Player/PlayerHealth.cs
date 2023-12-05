using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HealthBarController healthBar;
    [SerializeField] int maxHealth = 100;

    private Animator _animator;
    private Rigidbody2D _rb;

    private int _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnEnable()
    {
        EventManager.onPlayerDeath += Die;
    }
    private void OnDisable()
    {
        EventManager.onPlayerDeath -= Die;
    }

    public void TakeDamage(int damage)
    {
        // trigger animation
        _animator.SetTrigger("Hurt");
        // reduce health
        _currentHealth -= damage;
        healthBar.SetHealth(_currentHealth);
    }

    private void Die()
    {
        // trigger animation
        _animator.SetTrigger("Death");
        // disable colider
        GetComponent<PlayerController2D>().enabled = false;
        // disable attack
        GetComponent<PlayerCombat>().enabled = false;
        // stop moving
        _rb.velocity = Vector3.zero;
        // disable this script
        this.enabled = false;

    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
}
