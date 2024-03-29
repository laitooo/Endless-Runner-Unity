using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public GameObject panel;
    public Text scoreText;
    public string menuSceneName;

    void Start() {
        panel.SetActive(false);
    }

    void Update() {
        
    }

    public void toogleGameOver(float score) {
        float highScore = PlayerPrefs.GetInt("score", 0);
        if ((int) score > highScore) {
            PlayerPrefs.SetInt("score", (int) score);
        }
        scoreText.text = ((int) score).ToString();
        panel.SetActive(true);
        AudioManager.instance.stop("InGame");
        AudioManager.instance.play("InGameMenu");
    }

    public void RestartLevel() {
        AudioManager.instance.stop("InGameMenu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void goToMenu() {
        AudioManager.instance.stop("InGameMenu");
        SceneManager.LoadScene(menuSceneName);
    }
}
