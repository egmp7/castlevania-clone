using Game.Trackers;
using UnityEngine;

namespace Game.Managers
{
    [RequireComponent(typeof(StateTracker))]

    public class HealthManager : MonoBehaviour
    {

        [SerializeField] float initValue = 500;
        [Tooltip("Position of the health bar on the screen (X, Y)")] 
        [SerializeField] Vector2 healthBarPosition = new (10, 10);
        [Tooltip("Width and height of the health bar")]
        [SerializeField] Vector2 healthBarSize = new (200, 20);
        [SerializeField] float blockDamageReducer = 0.1f;

        [HideInInspector] public float currentHealth;

        private StateTracker _tracker;
        private float _initHealth;

        public void DecreaseHealth(float damage)
        {
            if (_tracker.IsBlockState()) damage *= blockDamageReducer;
            currentHealth -= damage;

            if (currentHealth < 0)
            {
                currentHealth = _initHealth;
                // Destroy(gameObject);
            }
        }

        private void Awake()
        {
            if (TryGetComponent(out _tracker))
            {
                Debug.LogError("BehaviorTree not initialized");
            }
        }

        private void Start()
        {
            currentHealth = initValue;
            _initHealth = initValue;
        }

        /// <summary>
        /// Draw the health bar on the screen using OnGUI
        /// </summary>
        private void OnGUI()
        {
            float healthPercentage = currentHealth / _initHealth;

            // Draw background bar (empty state)
            GUI.Box(new Rect(healthBarPosition.x, healthBarPosition.y, healthBarSize.x, healthBarSize.y), "");

            // Draw foreground bar (filled state)
            GUI.color = Color.red;  // Change the color of the health bar to red
            GUI.DrawTexture(new Rect(healthBarPosition.x, healthBarPosition.y, healthBarSize.x * healthPercentage, healthBarSize.y), Texture2D.whiteTexture);
            GUI.color = Color.white;  // Reset color after drawing
        }
    }
}

