using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public Text highScore;
    public Text coinsText;
    public string levelSceneName;

    void Start() {
        int score = PlayerPrefs.GetInt("score", 0);
        highScore.text = score.ToString();
        int coins = PlayerPrefs.GetInt("coins", 0);
        coinsText.text = coins.ToString();
        AudioManager.instance.play("MainMenu");
    }

    void Update() {
        
    }

    public void Play() {
        SceneManager.LoadScene(levelSceneName);
    }
}
