using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;    
    
    [HideInInspector] public bool isDead;

    private void Awake() {
        if (instance != null && instance != this) { 
            Destroy(this); 
        } else { 
            instance = this; 
        } 
        isDead = false;
    }

    private void Start() {
        AudioManager.instance.stop("MainMenu");
        AudioManager.instance.play("InGame");
    }

    public void loosedGame() {
        GetComponent<GameOver>().toogleGameOver(GetComponent<ScoreCounter>().score);
        isDead = true;
    }    
}
