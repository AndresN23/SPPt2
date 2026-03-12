using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;

    private float currentScore = 0;
    private float highScore = 0;
    private bool isGameOver = false;

    void Start()
    {
        Enemy.onEnemyDied += onEnemyDied;
        Enemy2.onEnemyDied += onEnemyDied;
        Enemy3.onEnemyDied += onEnemyDied;

        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        highScoreText.text = ((int)highScore).ToString("D4");

        gameOverPanel.SetActive(false);

        Instance = this;
    }

    void OnDestroy()
    {
        Enemy.onEnemyDied -= onEnemyDied;
        Enemy2.onEnemyDied -= onEnemyDied;
        Enemy3.onEnemyDied -= onEnemyDied;
    }

    void onEnemyDied(float score)
    {
        currentScore += score;
        scoreTxt.text = ((int)currentScore).ToString("D4");
    }

    public void PlayerDied()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreText.text = ((int)highScore).ToString("D4");
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                WinGame();
            }
        }

        if (isGameOver && Mouse.current.leftButton.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void WinGame()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreText.text = ((int)highScore).ToString("D4");
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        currentScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}