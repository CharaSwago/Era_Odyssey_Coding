using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public string level1;
    public string level2;
    public string level3;

    public GameObject settingsWindow;
    public GameObject Tuto;


    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
        public void LEVEL1()
    {
        SceneManager.LoadScene(level1);
    }
        public void LEVEL2()
    {
        SceneManager.LoadScene(level2);
    }
        public void LEVEL3()
    {
        SceneManager.LoadScene(level3);
    }

    public void Settings()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }
    public void closeTuto()
    {
        settingsWindow.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}