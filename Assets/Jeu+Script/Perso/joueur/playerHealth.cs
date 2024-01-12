using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isInvinsible = false;
    public SpriteRenderer graphics;
    public float invisiblityFlashDelay = 0.2f;
    public float invisiblityTimeAfterhit = 3;

    public healBar healBar;

    void Start()
    {
        currentHealth = maxHealth;
        healBar.SetMaxHealth(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            takeDamage(20);
        }
    }

    public void takeDamage (int damage)
    {
        if(!isInvinsible){
            currentHealth -= damage;
            healBar.SetHealth(currentHealth);
            isInvinsible = true;
            StartCoroutine(invisiblityFlash());
            StartCoroutine(handleInvincibilityDelay());
        }
    }

    public IEnumerator invisiblityFlash(){
        while(isInvinsible){
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invisiblityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invisiblityFlashDelay);
        }
    }
    public IEnumerator handleInvincibilityDelay(){
        yield return new WaitForSeconds(invisiblityTimeAfterhit);
        isInvinsible = false;
    }
}