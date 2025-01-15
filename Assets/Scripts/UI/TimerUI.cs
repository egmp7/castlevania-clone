using egmp7.Game.Manager;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("Timer UI Settings")]
    public Vector2 timerPosition = new (10, 50); // Position on the screen
    public int fontSize = 24; // Font size for the timer display

    private GUIStyle timerStyle;

    private void Start()
    {
        // Initialize the GUIStyle for the timer display
        timerStyle = new GUIStyle
        {
            fontSize = fontSize,
            normal = new GUIStyleState { textColor = Color.white }
        };
    }

    private void OnGUI()
    {
        // Retrieve the current timer value from MainManager
        float currentTime = MainManager.Instance.timer;

        // Format the time as minutes:seconds
        string formattedTime = FormatTime(currentTime);

        // Display the timer as a label
        GUI.Label(new Rect(timerPosition.x, timerPosition.y, 200, 50), $"Time: {formattedTime}", timerStyle);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60); // Calculate minutes
        int seconds = Mathf.FloorToInt(time % 60); // Calculate remaining seconds
        return $"{minutes:00}:{seconds:00}"; // Format as mm:ss
    }
}
