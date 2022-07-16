using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject panel;
    public string menuSceneName;

    void Start() {
        panel.SetActive(false);
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Escape) && !GameManager.instance.isDead) {
            panel.SetActive(true);
            Time.timeScale = 0;
            AudioManager.instance.pause("InGame");
            AudioManager.instance.play("InGameMenu");
        }
    }

    public void resumeTheGame() {
        panel.SetActive(false);    
        Time.timeScale = 1;
        AudioManager.instance.stop("InGameMenu");
        AudioManager.instance.play("InGame");
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     
        AudioManager.instance.stop("InGameMenu");
    }

    public void goToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuSceneName);
        AudioManager.instance.stop("InGameMenu");
    }
}
