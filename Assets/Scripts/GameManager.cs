using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public bool gameOver = false; // tracks if game ended
    public int score = 0;         // player score
    private bool isPaused = false; // pause state
    private bool musicOn = true;   // music toggle

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextShadow;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject settingsMenu;
    public GameObject settingsButton;

    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource scoreSound;

    void Start()
    {
        // Make sure game runs normally at start
        Time.timeScale = 1f;

        // Initialize UI elements
        if (scoreText != null)
            scoreText.text = "0";

        if (gameOverText != null)
            gameOverText.SetActive(false);

        if (restartButton != null)
            restartButton.SetActive(false);

        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        if (settingsButton != null)
            settingsButton.SetActive(true);

        // Make sure music starts unmuted
        if (musicSource != null)
            musicSource.mute = false;
    }

    void Update()
    {
        // Do nothing if game is over
        if (gameOver) return;

        // Press ESC to pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Called when player scores
    public void AddScore()
    {
        if (gameOver) return;

        // Increase score
        score++;

        // Update UI text
        if (scoreText != null)
            scoreText.text = score.ToString();
        if (scoreTextShadow != null)
            scoreTextShadow.text = score.ToString();

        // Play score sound
        if (scoreSound != null)
            scoreSound.Play();
    }

    // Called when player loses
    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;
        isPaused = false;

        // Show game over UI
        if (gameOverText != null)
            gameOverText.SetActive(true);

        if (restartButton != null)
            restartButton.SetActive(true);

        // Hide settings
        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        if (settingsButton != null)
            settingsButton.SetActive(false);

        // Freeze game
        Time.timeScale = 0f;
    }

    // Toggle pause state
    public void TogglePause()
    {
        isPaused = !isPaused;

        // Show/hide settings menu
        if (settingsMenu != null)
            settingsMenu.SetActive(isPaused);

        if (settingsButton != null)
            settingsButton.SetActive(!isPaused);

        // Pause or resume game time
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // Resume game manually
    public void ResumeGame()
    {
        isPaused = false;

        if (settingsMenu != null)
            settingsMenu.SetActive(false);

        if (settingsButton != null)
            settingsButton.SetActive(true);

        Time.timeScale = 1f;
    }

    // Turn music on/off
    public void ToggleMusic()
    {
        musicOn = !musicOn;

        if (musicSource != null)
            musicSource.mute = !musicOn;
    }

    // Restart the scene
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}