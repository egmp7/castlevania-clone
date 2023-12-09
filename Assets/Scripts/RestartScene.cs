using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.onPlayerDeath += Restart;
    }
    private void OnDisable()
    {
        EventManager.onPlayerDeath -= Restart;
    }

    private void Restart()
    {
        Invoke("LoadScene", 2f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }
}
