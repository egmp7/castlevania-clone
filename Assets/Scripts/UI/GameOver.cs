using egmp7.Game.Manager;
using TMPro;
using UnityEngine;

namespace egmp7.Game.UI
{
    public class GameOverUI : MonoBehaviour
    {

        [Header("UI Components")]
        [Tooltip("Reference to the TextMeshProUGUI component for displaying the final score.")]
        public TextMeshProUGUI finalScoreText;

        [Header("Settings")]
        [Tooltip("Prefix text to display before the score.")]
        public string scorePrefix = "Final Score: ";

        private void Awake()
        {
            if (finalScoreText == null)
            {
                Debug.LogError("FinalScoreText is not assigned in the inspector.");
                return;
            }
        }

        /// <summary>
        /// Updates the TextMeshProUGUI component to show the final score.
        /// </summary>
        /// <param name="score">The final score to display.</param>
        public void Render()
        {
            gameObject.SetActive(true);
            if (finalScoreText != null)
            {
                finalScoreText.text = scorePrefix + MainManager.Instance.score.ToString();
            }
        }
    }
}

