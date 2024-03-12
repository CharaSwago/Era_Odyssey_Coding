using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    public int coinsCount;
    public Text coinsCountText;
    public static inventory instance;

    private void Awake(){
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }
        instance = this;
    }
    public void addCoins(int count){
        coinsCount += count;
        coinsCountText.text = coinsCount.ToString();
    }
}