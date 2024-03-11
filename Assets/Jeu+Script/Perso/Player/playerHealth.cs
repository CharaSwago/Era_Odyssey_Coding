using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;
    public float invincibilityTimeAfterHit = 2f;
    public float invincibilityFlashDelay = 0.2f;
    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public healBar healthBar;
    public static playerHealth instance; // Définition de la variable statique instance

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(100);
        }
    }

    public void TakeDamage(int Damage)
    {
        if (!isInvincible)
        {
            currentHealth -= Damage;
            healthBar.SetHealth(currentHealth);

            if(currentHealth <=0)
            {
                Die();
                return;
            }
        }
    }

    public void Die ()
    {
        movePlayer.instance.enabled = false;
        movePlayer.instance.animator.SetTrigger("Die");
        movePlayer.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        movePlayer.instance.rb.velocity = Vector3.zero;
        movePlayer.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        movePlayer.instance.enabled = true;
        movePlayer.instance.animator.SetTrigger("Respawn");
        movePlayer.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        movePlayer.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay); // Réduit le temps de flash à 0.1s pour un effet plus rapide
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }

        // Assurez-vous de réinitialiser la couleur après la fin de l'invincibilité
        graphics.color = new Color(1f, 1f, 1f, 1f);
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);  
        isInvincible = false;
    }
}