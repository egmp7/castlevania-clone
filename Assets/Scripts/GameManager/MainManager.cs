using UnityEngine;

namespace egmp7.Game.Manager
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;

        public int score = 0; // Tracks the player's score
        public float timer = 120f; // Timer starts at 120 seconds (2 minutes)
        public bool isTimerRunning = true; // Controls whether the timer is active

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Debug.Log("New instance initialized...");
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                Debug.Log("Duplicate instance found and deleted...");
            }
        }

        void Update()
        {
            // Countdown logic for the timer
            if (isTimerRunning && timer > 0)
            {
                timer -= Time.deltaTime;

                // Clamp the timer at 0 to avoid negative values
                if (timer <= 0)
                {
                    timer = 0;
                    isTimerRunning = false; // Stop the timer when it reaches 0
                    OnTimerEnd(); // Trigger an event when the timer ends
                }
            }
        }

        private void OnTimerEnd()
        {
            Debug.Log("Timer has ended!");
            // Add additional logic here, such as ending the game or triggering an event
        }
    }
}
