using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action onPlayerDeath;

    [SerializeField] PlayerHealth playerHealth;

    void Update()
    {
        if (playerHealth.GetCurrentHealth() <= 0)
        {
            onPlayerDeath?.Invoke();
        }
    }
}
