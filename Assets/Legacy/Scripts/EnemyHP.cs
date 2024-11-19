using UnityEngine;

namespace Legacy.TestEnemy
{

    public class EnemyHP : MonoBehaviour
    {
        private float _currentHealth;
        private float _initHealth;

        // Position of the health bar on the screen (X, Y)
        public Vector2 healthBarPosition = new Vector2(10, 10);
        // Width and height of the health bar
        public Vector2 healthBarSize = new Vector2(200, 20);

        private void Start()
        {
            float _initValue = 500;

            _currentHealth = _initValue;
            _initHealth = _initValue;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth < 0)
            {
                _currentHealth = _initHealth;
               // Destroy(gameObject);
            }
        }

        // Draw the health bar on the screen using OnGUI
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
    }


}
