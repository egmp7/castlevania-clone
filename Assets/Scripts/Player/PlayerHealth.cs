using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HealthBarController healthBar;
    [SerializeField] int maxHealth = 100;
    [SerializeField] float noDamageRange = 1f;

    private Animator _animator;
    private Rigidbody2D _rb;

    private int _currentHealth;
    private bool _canTakeDamage = true;
    private float _nextEnabled;


    void Start()
    {
        _currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Time.time > _nextEnabled) _canTakeDamage = true;
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
        if (!_canTakeDamage) return;

        // trigger animation
        _animator.SetTrigger("Hurt");
        // reduce health
        _currentHealth -= damage;
        healthBar.SetHealth(_currentHealth);
        _canTakeDamage= false;
        _nextEnabled = Time.time + noDamageRange;
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
