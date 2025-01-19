using egmp7.Game.Manager;
using Game.AnimationEvent.Source;
using Player.StateManagement;
using UnityEngine;

namespace Game.Managers
{
    public class HealthManagerSM : HealthManager
    {
        private StateMachine _stateMachine;
        private DamageProcessor _processor;

        private void Awake()
        {
            _stateMachine = GetRequiredComponent<StateMachine>();
            _processor = GetRequiredComponent<DamageProcessor>();
        }

        private T GetRequiredComponent<T>() where T : Component
        {
            if (TryGetComponent<T>(out var component))
            {
                return component;
            }

            ErrorManager.LogMissingComponent<T>(gameObject);
            return null; // Optionally throw an exception or handle this scenario differently
        }


        public void DecreaseHealth()
        {
            // Retrieve the damage amount from the processor
            float damage = _processor.GetToAttack().amount;

            // Check if the current state is BlockState
            if (_stateMachine.GetCurrentState() is BlockState blockState)
            {
                // Play block animation and reduce damage
                blockState.PlayBlockAnimation();
                damage *= blockDamageReducer;
            }
            else
            {
                // Trigger hurt behavior for other states
                _stateMachine.Hurt();
            }

            // Apply damage to current health
            _currentHealth -= damage;

            if (_currentHealth < 0)
            {
                // Reset health if it drops below zero (prevent negative health)
                _currentHealth = _initHealth;
                MainManager.OnGameOver?.Invoke();
            }
        }
    }
}

