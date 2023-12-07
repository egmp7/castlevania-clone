using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action onPlayerDeath;

    [SerializeField] GameObject player;
    
    private PlayerHealth _playerHealth;
    private PlayerController2D _playerController;

    private void Start()
    {
        _playerHealth = player.GetComponent<PlayerHealth>();
        _playerController = player.GetComponent<PlayerController2D>();
    }

    void Update()
    {
        if (_playerHealth.GetCurrentHealth() <= 0 || _playerController.isOutOfBoundary() )
        {
            onPlayerDeath?.Invoke();
        }
    }
}
