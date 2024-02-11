using UnityEngine;
using UnityEngine.SceneManagement;

public class Parchemin : MonoBehaviour
{
    public GameObject ParcheminCanvas;
    private string sceneName;

    private void Start()
    {
        // Récupérer le nom de la scène actuelle
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Sauvegarder l'état du niveau
            SauvegarderEtatNiveau();

            // Afficher le Canvas
            ParcheminCanvas.SetActive(true);

            // Mettre en pause le jeu (si nécessaire)
            Time.timeScale = 0f;

            // Détruire le parchemin
            Destroy(gameObject);
        }
    }

    public void BoutonRetour()
    {
        // Charger l'état du niveau
        ChargerEtatNiveau();

        // Recharger la scène
        SceneManager.LoadScene(sceneName);

        // Reprendre le jeu
        Time.timeScale = 1f;
    }

    private void SauvegarderEtatNiveau()
    {
        // Sauvegarder des informations sur l'état du niveau
        PlayerPrefs.SetInt("NiveauCourant", SceneManager.GetActiveScene().buildIndex);
    }

    private void ChargerEtatNiveau()
    {
        // Charger l'index de la scène du joueur
        int niveauCourant = PlayerPrefs.GetInt("NiveauCourant", 0);

        // Charger la scène du joueur
        SceneManager.LoadScene(niveauCourant);
    }
}