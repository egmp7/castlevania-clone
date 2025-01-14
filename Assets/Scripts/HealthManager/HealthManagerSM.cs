using Game.AnimationEvent.Source;
using Player.StateManagement;

namespace Game.Managers
{
    public class HealthManagerSM : HealthManager
    {
        private StateMachine _stateMachine;
        private DamageProcessor _processor;

        private void Awake()
        {
            if (!TryGetComponent(out _stateMachine))
            {
                ErrorManager.LogMissingComponent<StateMachine>(gameObject);
            }

            if (!TryGetComponent(out _processor))
            {
                ErrorManager.LogMissingComponent<DamageProcessor>(gameObject);
            }
        }

        public void DecreaseHealth()
        {
            float damage = _processor.GetToAttack().amount;

            if (_stateMachine.GetCurrentState() is BlockState blockState)
            {
                blockState.PlayBlockAnimation();
                damage *= blockDamageReducer;
            }
            else
            {
                _stateMachine.Hurt();
            }

            _currentHealth -= damage;

            if (_currentHealth < 0)
            {
                _currentHealth = _initHealth;
                // Destroy(gameObject);
            }
        }
    }
}

