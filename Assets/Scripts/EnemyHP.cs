using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
