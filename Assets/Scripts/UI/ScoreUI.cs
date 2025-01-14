using egmp7.Game.Manager;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [Header("Score UI Settings")]
    public Vector2 scorePosition = new (10, 10); // Position on the screen
    public int fontSize = 24; // Font size for the score

    private GUIStyle scoreStyle;

    private void Start()
    {
        // Initialize the GUIStyle for the score display
        scoreStyle = new GUIStyle
        {
            fontSize = fontSize,
            normal = new GUIStyleState { textColor = Color.white }
        };
    }

    private void OnGUI()
    {
        // Display the score as a label
        GUI.Label(new Rect(scorePosition.x, scorePosition.y, 200, 50), $"Score: {MainManager.Instance.score}", scoreStyle);
    }
}
