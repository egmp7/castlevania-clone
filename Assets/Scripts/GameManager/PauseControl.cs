using InputCommands.Buttons;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool isPaused;
    private float previousTimeScale = 1;
    private ButtonPause _buttonPause;

    private void Awake()
    {
        _buttonPause = new ButtonPause();
    }

    private void Update()
    {
        if (_buttonPause.IsPressed())
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (Time.timeScale > 0 )
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
            isPaused = true;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            isPaused = false;
        }
    }
}   
