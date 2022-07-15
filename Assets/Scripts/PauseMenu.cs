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
        }
    }

    public void resumeTheGame() {
        panel.SetActive(false);    
        Time.timeScale = 1;   
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void goToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuSceneName);
    }

}
