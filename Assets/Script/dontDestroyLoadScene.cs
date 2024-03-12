using UnityEngine;
using UnityEngine.SceneManagement;

public class dontDestroyLoadScene : MonoBehaviour
{
    public GameObject[] objects;

    public static dontDestroyLoadScene instance;
    private void Awake ()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroyOnLoadScene dans scÃ¨ne");
            return;
        }
        instance = this;

        foreach (var element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objects)
        {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
        }
    }
}
