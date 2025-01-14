using Game.Managers;
using UnityEngine;

namespace egmp7.Game.UI
{
    public class PlayerHealthUI : MonoBehaviour
    {

        [SerializeField] HealthManagerSM healthManager;

        [Tooltip("Position of the health bar on the screen (X, Y)")]
        [SerializeField] Vector2 healthBarPosition = new(10, 10);
        [Tooltip("Width and height of the health bar")]
        [SerializeField] Vector2 healthBarSize = new(200, 20);

        private void Start()
        {
            if (healthManager == null)
            {
                ErrorManager.LogMissingComponent<HealthManagerSM>(gameObject);
            }
        }

        /// <summary>
        /// Draw the health bar on the screen using OnGUI
        /// </summary>
        private void OnGUI()
        {
            float healthPercentage = healthManager.GetHealthPercentage();
            // Draw background bar (empty state)
            GUI.Box(new Rect(healthBarPosition.x, healthBarPosition.y, healthBarSize.x, healthBarSize.y), "");

            // Draw foreground bar (filled state)
            GUI.color = Color.red;  // Change the color of the health bar to red
            GUI.DrawTexture(new Rect(healthBarPosition.x, healthBarPosition.y, healthBarSize.x * healthPercentage, healthBarSize.y), Texture2D.whiteTexture);
            GUI.color = Color.white;  // Reset color after drawing
        }
    }
}
