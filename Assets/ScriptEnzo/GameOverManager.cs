using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans scÃ¨ne");
            return;
        }
        instance = this;
    }
    public GameObject gameOverUI;
    public void OnPlayerDeath()
    {
        if(CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            dontDestroyLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }

        gameOverUI.SetActive(true);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerHealth.instance.Respawn();
        gameOverUI.SetActive(false);
    }

    public void MenuButton()
    {
        dontDestroyLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene("MenuPrincipale");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
