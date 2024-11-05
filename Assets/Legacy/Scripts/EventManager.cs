using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action onPlayerDeath;

    [SerializeField] GameObject player;
    
    private PlayerController2D _playerController;

    private void Start()
    {
        _playerController = player.GetComponent<PlayerController2D>();
    }

    void Update()
    {
        if (_playerController.GetCurrentHealth() <= 0 || _playerController.isOutOfBoundary() )
        {
            onPlayerDeath?.Invoke();
        }
    }
}
