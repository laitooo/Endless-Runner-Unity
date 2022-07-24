using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {
    public PlayerMovement playerMovement;
    public Text scoreText;
    public int levelUpScrore = 10;
    public int maxLevel = 10;
    [HideInInspector] public float score = 0f;

    private int level = 1;
    private int scoreToNextLevel;

    void Start() {
        scoreText.text = "0";
        scoreToNextLevel = levelUpScrore;
    }

    void Update() {
        if (!GameManager.instance.isDead) {
            if (score >= scoreToNextLevel) {
                levelUp();
            }
            score += (Time.deltaTime * (1f + (level - 1) / 10f));
            scoreText.text = ((int) score).ToString();
        }
    }

    private void levelUp() {
        if (level == maxLevel) {
            return;
        }
        scoreToNextLevel += levelUpScrore;
        level++;
        playerMovement.increaseSpeed((level - 1) * 50);
    }
}
