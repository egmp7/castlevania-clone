using UnityEngine;

namespace Game.Managers
{
    public class HealthManagerBT : HealthManager
    {
        public Vector2 healthBarSize = new (50, 5); // Size of the health bar
        public Vector2 healthBarOffset = new (0, 100); // Offset above the enemy in world units

        private Camera mainCamera; // Reference to the main camera

        private void Awake()
        {
            mainCamera = Camera.main; // Get the main camera
        }

        /// <summary>
        /// Draw the health bar on the screen using OnGUI
        /// </summary>
        private void OnGUI()
        {
            // Calculate the health percentage
            float healthPercentage = _currentHealth / _initHealth;

            // Get the enemy's position in screen space
            Vector3 worldPosition = transform.position + (Vector3)healthBarOffset;
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition);

            // Flip Y coordinate for GUI
            screenPosition.y = Screen.height - screenPosition.y;

            // Define the position and size of the health bar
            Rect healthBarPosition = new Rect(
                screenPosition.x - healthBarSize.x / 2, // Center the health bar horizontally
                screenPosition.y - healthBarSize.y / 2, // Align the health bar vertically
                healthBarSize.x,
                healthBarSize.y
            );

            // Draw background bar (empty state)
            GUI.Box(healthBarPosition, "");

            // Draw foreground bar (filled state)
            GUI.color = Color.red; // Change the color of the health bar to red
            GUI.DrawTexture(new Rect(
                healthBarPosition.x,
                healthBarPosition.y,
                healthBarSize.x * healthPercentage,
                healthBarSize.y
            ), Texture2D.whiteTexture);
            GUI.color = Color.white; // Reset color after drawing
        }
        public void DecreaseHealth(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth < 0)
            {
                _currentHealth = 0;
                //_currentHealth = _initHealth;
            }
        }
    }
}
