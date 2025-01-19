using egmp7.Game.UI;
using UnityEngine;

namespace egmp7.Game.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameOverUI GameOverUI;

        private void Awake()
        {
            if (GameOverUI == null)
            {
                ErrorManager.LogMissingComponent<GameOverUI>(gameObject);
            }
        }

        private void OnEnable()
        {
            MainManager.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            MainManager.OnGameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            GameOverUI.Render();
        }
    }
}
