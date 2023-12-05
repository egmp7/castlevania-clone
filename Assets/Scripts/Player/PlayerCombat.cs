using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] int enemyAttack = 30;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] float attackRate = 2.0f;
    
    private Animator _animator;
    private float _nextAttackTime = 0.0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (canPlayerAttack() && Input.GetButtonDown("Fire1"))
        {
            Attack();
            _nextAttackTime = Time.time + 1/attackRate;
        }
    }

    public void AttackOnAnimation()
    {
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damaged them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BanditController>().TakeDamage(enemyAttack);
        }
    }

    private bool canPlayerAttack()
    {
        if (Time.time > _nextAttackTime) return true;
        return false;
    }

    private void Attack()
    {
        // play attack animation
        _animator.SetTrigger("Attack");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
