using InputCommands.Buttons;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace egmp7.Game.Manager
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;

        public static Action OnRestart;
        public static Action OnGameOver;

        // Score
        public int score = 0; // Tracks the player's score
        
        // Timer
        public float timer = 120f; // Timer starts at 120 seconds (2 minutes)
        private bool _isTimerRunning = true; // Controls whether the timer is active

        // Difficulty
        public float difficulty = 0f; // Current difficulty level
        [SerializeField] float IncrementInterval = 10f; // Interval to increase difficulty
        [SerializeField] float GameDuration;
        private float _elapsedTime = 0f; // Time elapsed since the game started

        private ButtonRestart _buttonRestart;

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

            _buttonRestart = new ButtonRestart();
        }

        private void Start()
        {
            GameDuration = timer;
            StartDifficultyIncrement(0.5f);
        }

        void Update()
        {
            if (_buttonRestart.IsPressed())
            {
                OnRestart?.Invoke();
                Restart();
            }

            // Countdown logic for the timer
            if (_isTimerRunning && timer > 0)
            {
                timer -= Time.deltaTime;

                // Clamp the timer at 0 to avoid negative values
                if (timer <= 0)
                {
                    timer = 0;
                    _isTimerRunning = false; // Stop the timer when it reaches 0
                    OnTimerEnd(); // Trigger an event when the timer ends
                }
            }
        }

        private void OnTimerEnd()
        {
            Debug.Log("Timer has ended!");
            OnGameOver?.Invoke();
            // Add additional logic here, such as ending the game or triggering an event
        }

        // Method to calculate difficulty increment
        public void StartDifficultyIncrement(float incrementPerInterval)
        {
            StartCoroutine(IncrementDifficulty(incrementPerInterval));
        }

        private IEnumerator IncrementDifficulty(float incrementPerInterval)
        {
            while (_elapsedTime < GameDuration)
            {
                yield return new WaitForSeconds(IncrementInterval);
                _elapsedTime += IncrementInterval;
                difficulty += incrementPerInterval;
                Debug.Log($"Difficulty Increased: {difficulty}");
            }
            Debug.Log("Game Over");
        }

        public void Restart()
        {
            _elapsedTime = 0;
            difficulty = 0;
            timer = 120;
            score = 0;
            Utilities.DestroyAllWithTag("Enemy");
            // Reload the current active scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
