using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Enemy")
        {
            playerHealth.TakeDamage(50);
        }
    }
    
}
