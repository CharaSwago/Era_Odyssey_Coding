using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            inventory.instance.addCoins(1);
            Destroy(gameObject); 
        }
    }
}
