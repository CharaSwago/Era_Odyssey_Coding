using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class loadSpecificScene : MonoBehaviour
{
    public string sceneName;
    public Animator fadeSystem;

    private void Awake(){
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            StartCoroutine(loadNextScene());
        }
    }

    public IEnumerator loadNextScene()
    {
        fadeSystem.SetTrigger("FonduEnNoir");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
