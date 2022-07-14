using UnityEngine;
using UnityEngine.UI;

public class CoinsManager : MonoBehaviour {

    public static CoinsManager instance;
    
    private void Awake() {
        if (instance != null && instance != this) { 
            Destroy(this); 
        } else { 
            instance = this; 
        } 
    }

    [HideInInspector] public int coins;
    public Text coinsText;

    void Start() {
        coins = PlayerPrefs.GetInt("coins", 0);
        coinsText.text = coins == 0 ? "" : coins.ToString() + " $";
    }

    public void collectCoin() {
        coins += 1;
        PlayerPrefs.SetInt("coins", coins);
        coinsText.text = coins.ToString() + " $";
    }
}
