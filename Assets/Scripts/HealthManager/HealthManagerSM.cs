using Game.AnimationEvent.Source;
using Player.StateManagement;
using UnityEngine;

namespace Game.Managers
{
    public class HealthManagerSM : HealthManager
    {
        [Tooltip("Position of the health bar on the screen (X, Y)")]
        [SerializeField] Vector2 healthBarPosition = new(10, 10);
        [Tooltip("Width and height of the health bar")]
        [SerializeField] Vector2 healthBarSize = new(200, 20);

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

        /// <summary>
        /// Draw the health bar on the screen using OnGUI
        /// </summary>
        private void OnGUI()
        {
            float healthPercentage = _currentHealth / _initHealth;

            // Draw background bar (empty state)
            GUI.Box(new Rect(healthBarPosition.x, healthBarPosition.y, healthBarSize.x, healthBarSize.y), "");

            // Draw foreground bar (filled state)
            GUI.color = Color.red;  // Change the color of the health bar to red
            GUI.DrawTexture(new Rect(healthBarPosition.x, healthBarPosition.y, healthBarSize.x * healthPercentage, healthBarSize.y), Texture2D.whiteTexture);
            GUI.color = Color.white;  // Reset color after drawing
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

