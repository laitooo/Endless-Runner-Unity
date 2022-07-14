using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public Text highScore;
    public string levelSceneName;

    void Start() {
        int score = PlayerPrefs.GetInt("score", 0);
        highScore.text = score == 0 ? "" : "High score : " + score.ToString();
    }

    void Update() {
        
    }

    public void Play() {
        SceneManager.LoadScene(levelSceneName);
    }
}
